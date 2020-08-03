# IP opslaan in een txt file. apparaat.txt bestaat al met de apparaatnaam
echo "IP: " >~/extra.txt
/sbin/ifconfig | grep -Eo 'inet (addr:)?([0-9]*\.){3}[0-9]*' | grep -Eo '([0-9]*\.){3}[0-9]*' | grep -v '127.0.0.1' >> ~/extra.txt
    
# Uptime opslaan in een txt
echo "Uptime: " >>~/extra.txt
/usr/bin/uptime | grep -ohe 'up .*' | sed 's/,//g' | awk '{ print $2 }' >> ~/extra.txt

# Beschikbare schijfruimte naar txt
echo "Free diskspace: " >> ~/extra.txt
/bin/df -h | tr -s ' ' $'\t' | grep /dev/root | cut -f4 >> ~/extra.txt
    
# Ping versturen naar de server
curl -d "device=$(cat ~/apparaat.txt)&info=$(cat ~/extra.txt)" -X POST $DEVICE_TRACKER_URL/ping/ping
