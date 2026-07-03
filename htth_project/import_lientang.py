import os
import sys
import re
import subprocess

def parse_config(config_path):
    config = {}
    if not os.path.exists(config_path):
        print(f"Config file not found: {config_path}")
        return config
    with open(config_path, 'r', encoding='utf-8') as f:
        for line in f:
            line = line.strip()
            if not line or line.startswith('#'):
                continue
            parts = line.split(':', 1)
            if len(parts) == 2:
                key = parts[0].strip()
                val = parts[1].strip()
                config[key] = val
    return config

def main():
    import sys
    try:
        sys.stdout.reconfigure(encoding='utf-8')
    except AttributeError:
        pass

    config = parse_config('htth.conf')
    host = config.get('mysql-host', 'localhost')
    user = config.get('mysql-user', 'root')
    password = config.get('mysql-password', '')
    database = config.get('mysql-database', 'htth')

    print(f"Database settings from htth.conf:")
    print(f"  Host: {host}")
    print(f"  User: {user}")
    print(f"  DB:   {database}")

    # Check if pymysql is installed, otherwise try installing it
    try:
        import pymysql
    except ImportError:
        print("pymysql is not installed. Installing pymysql package...")
        try:
            subprocess.check_call([sys.executable, "-m", "pip", "install", "pymysql"])
            import pymysql
        except Exception as e:
            print(f"Error: Could not install pymysql package: {e}")
            print("Please run 'pip install pymysql' manually and run this script again.")
            sys.exit(1)

    print("Connecting to MySQL...")
    try:
        connection = pymysql.connect(
            host=host,
            user=user,
            password=password,
            database=database,
            charset='utf8mb4',
            cursorclass=pymysql.cursors.DictCursor
        )
        print("Connected successfully!")
    except Exception as e:
        print(f"Connection failed: {e}")
        sys.exit(1)

    # Read SQL file
    sql_path = 'lientang_maps.sql'
    if not os.path.exists(sql_path):
        print(f"SQL file not found: {sql_path}")
        sys.exit(1)

    with open(sql_path, 'r', encoding='utf-8') as f:
        sql_content = f.read()

    # Split commands by semicolon
    # We should split on semicolons that are not inside quotes
    # For this simple script, we can just split on ';\\s*\\n' or similar, or split by ';' and filter
    # But since each INSERT query is on its own line and ends with ';', we can use regex split.
    statements = re.split(r';\s*\n', sql_content)

    with connection.cursor() as cursor:
        for stmt in statements:
            stmt = stmt.strip()
            if not stmt:
                continue
            
            # Print query preview
            preview = stmt.split('\n')[0]
            if len(preview) > 80:
                preview = preview[:77] + "..."
            print(f"Executing: {preview}")
            
            try:
                cursor.execute(stmt)
            except Exception as e:
                print(f"Error executing statement:\n{stmt[:200]}...\nReason: {e}")
                connection.rollback()
                sys.exit(1)

    connection.commit()
    connection.close()
    print("Database import completed successfully!")

if __name__ == '__main__':
    main()
