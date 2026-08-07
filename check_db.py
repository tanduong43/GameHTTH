import re

with open(r"d:\project\GameHTTH\temp_player.java", "r", encoding="utf-8", errors="ignore") as f:
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

flush_code = extract_method("flush")
setup_code = extract_method("setup")

with open(r"d:\project\GameHTTH\flush.txt", "w", encoding="utf-8") as f:
    f.write(flush_code)
with open(r"d:\project\GameHTTH\setup.txt", "w", encoding="utf-8") as f:
    f.write(setup_code)
