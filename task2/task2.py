import hashlib
import os
f_names = os.listdir()
dir = os.getcwd()
for i in range(len(f_names)):
    fn = f_names[i]
    d = str(os.getcwd()) +'/' + fn
    f = open(d, encoding = "ISO-8859-1").read()
    s = hashlib.sha3_256(f.encode('utf-8')).hexdigest()
    print(fn, s)