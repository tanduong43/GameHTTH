import pymysql
import json
import sys
sys.stdout.reconfigure(encoding='utf-8')

conn = pymysql.connect(host="localhost", user="root", password="123456", database="htth")
cur = conn.cursor()

cur.execute("SELECT id, name, danhhieu, list_danhhieu, point_inven FROM players WHERE name=%s", ("adminne3",))
r = cur.fetchone()
print("id:", r[0])
print("name:", r[1])
print("danhhieu:", r[2])
print("list_danhhieu:", r[3])
pi = r[4]
print("point_inven:", pi)
try:
    arr = json.loads(pi)
    print("point_inven size:", len(arr))
    for i, v in enumerate(arr):
        print(f"  [{i}] = {v}")
except Exception as e:
    print("parse error", e)

# also duongka
cur.execute("SELECT id, name, danhhieu, list_danhhieu, point_inven FROM players WHERE name=%s", ("duongka",))
r = cur.fetchone()
print("\n=== duongka ===")
print("danhhieu:", r[2], "list:", r[3])
try:
    arr = json.loads(r[4])
    print("point_inven size:", len(arr))
    if len(arr) > 12:
        print("  [12] =", arr[12])
except Exception as e:
    print("parse error", e)

cur.execute("SELECT COUNT(*) FROM danhhieu")
print("\ndanhhieu templates:", cur.fetchone()[0])
conn.close()
