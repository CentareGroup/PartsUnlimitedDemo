#
# Cookbook Name:: mrpapp-idempotent
# Recipe:: default
#
# Copyright (c) 2015 The Authors, All Rights Reserved.

# Runs apt-get update
include_recipe 'apt'

# Add the JDK install
include_recipe 'java'

# Install MongoDB
include_recipe 'mongodb'

# Setup Tomcat
mrp_name = 'mrp'
tomcat_install mrp_name

template "/opt/tomcat_#{mrp_name}/conf/server.xml" do
  source 'server.xml.erb'
  owner 'root'
  group 'root'
  mode '0644'
  variables ({
    :port => "9080",
    :shutdown_port => "8007"
  })
  notifies :restart, "tomcat_service[#{mrp_name}]"
end

tomcat_service mrp_name do
  action :start
end

# Setup services for springboot apps
project = "ordering-service"
template "/etc/init.d/#{project}" do
  source 'spring-boot-service.sh.erb'
  owner 'root'
  group 'root'
  mode '755'
  variables ({ :project => project })
end
directory "/var/log/#{project}"

project = "integration-service"
template "/etc/init.d/#{project}" do
  source 'spring-boot-service.sh.erb'
  owner 'root'
  group 'root'
  mode '755'
  variables ({ :project => project })
end
directory "/var/log/#{project}"
