import os

def main():
    base_dir = r"d:\project\GameHTTH\htth_project"
    sql_path = os.path.join(base_dir, "database", "database.sql")
    
    with open(sql_path, "r", encoding="utf-8") as f:
        for line in f:
            if "Enel" in line:
                print(line.strip())

if __name__ == "__main__":
    import sys
    sys.stdout.reconfigure(encoding='utf-8')
    main()
