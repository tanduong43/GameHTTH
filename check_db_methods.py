import re

with open(r"d:\project\GameHTTH\CodeTemp\client\Player.java", "r", encoding="utf-8", errors="ignore") as f:
    content = f.read()

def extract_method(method_name):
    matches = re.finditer(r'(?:public|protected|private|\s)+\w*\s*' + method_name + r'\s*\([^\)]*\)\s*(?:throws\s+[\w\,\s]+)?\s*\{', content)
    for match in matches:
        start = match.start()
        brace = 0
        for i in range(start, len(content)):
            if content[i] == '{': brace += 1
            elif content[i] == '}':
                brace -= 1
                if brace == 0:
                    return content[start:i+1]
    return ""

print("==== LOAD ====")
load_str = extract_method("setup")
if not load_str: load_str = extract_method("login")
print(load_str[:500])
print("==== FLUSH ====")
flush_str = extract_method("flush")
print(flush_str[:500])
