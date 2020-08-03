ssh -t ovh "sudo service device stop && sudo chown -R niek /opt/Device"
rsync -avz  DeviceTracker/bin/Release/netcoreapp3.1/publish/* ovh:/opt/Device/
ssh -t ovh "sudo chown -R www-data /opt/Device && sudo service device start"
