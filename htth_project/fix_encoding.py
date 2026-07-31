import re

def fix_mojibake(match):
    text = match.group(0)
    try:
        # Check if it contains typical mojibake characters
        if any(c in text for c in ['Ã', 'Ä', 'á', '»', '£', '¿', '½', '¼', 'Æ', '¢']):
            # Only fix the inner string, leave the quotes
            inner = text[1:-1]
            fixed_inner = inner.encode('windows-1252').decode('utf-8')
            return f'"{fixed_inner}"'
    except Exception:
        pass
    return text

with open('src/main/java/core/Service.java', 'r', encoding='utf-8') as f:
    content = f.read()

fixed_content = re.sub(r'"[^"\\]*(?:\\.[^"\\]*)*"', fix_mojibake, content)

with open('src/main/java/core/Service.java', 'w', encoding='utf-8') as f:
    f.write(fixed_content)

print("Fixed Service.java encoding issues.")
