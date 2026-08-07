import re
with open(r"d:\project\GameHTTH\htth_project\src\main\java\client\Player.java", "r", encoding="utf-8") as f:
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

flush = extract_method("flush")
print("==== flush json example ====")
print('\n'.join([line for line in flush.split('\n') if 'JSONArray js =' in line or 'js.add' in line][:15]))
