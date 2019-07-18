

from __future__ import unicode_literals
from fabric import *

def host_type():
    run('uname -s')

def hello(name="world"):
    print("Hello %s!" % name)

def composite(name="world"):
    hello(name)
    host_type()

composite("zhdj-test") 