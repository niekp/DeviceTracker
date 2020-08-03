ssh -t ovh "sudo service devicetracker stop && sudo chown -R niek /opt/DeviceTracker"
rsync -avz  DeviceTracker/bin/Release/netcoreapp3.1/publish/* ovh:/opt/DeviceTracker/
ssh -t ovh "sudo chown -R www-data /opt/DeviceTracker && sudo service devicetracker start"
