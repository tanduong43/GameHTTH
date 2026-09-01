import os
import glob
from PIL import Image

for pattern in [
    'htth_project/data/danhhieu/effect/*/*',
    'htth_project/data/nro/data/effect/*/*',
    'htth_project/data/danhhieu/*/*',
    'htth_project/data/template/skill/*/*'
]:
    for f in glob.glob(pattern):
        if any(k in f for k in ['72', '17', '40', '64', '65', '67', '68', '73', '85', '87', '88', '89']):
            try:
                sz = os.path.getsize(f)
                if f.endswith('.png'):
                    im = Image.open(f)
                    print(f, im.size, sz)
                else:
                    print(f, "DATA", sz)
            except Exception:
                pass
