import requests
import os

response = requests.get(os.environ['PING_URL'] + "/?device=" + os.environ['DEVICE'])
