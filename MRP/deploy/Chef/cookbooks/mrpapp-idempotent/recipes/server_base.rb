#
# Cookbook Name:: mrpapp-idempotent
# Recipe:: server_base
#
# Copyright (c) 2015 The Authors, All Rights Reserved.

# Runs apt-get update
include_recipe 'apt'

# Add the Open JDK apt repo
apt_repository 'openJDK' do
  uri 'ppa:openjdk-r/ppa'
  distribution 'trusty'
end

# Install JDK and JRE
apt_package 'openjdk-8-jdk' do
  action :install
end

apt_package 'openjdk-8-jre' do
  action :install
end

# Set Java environment variables
base_path = '/usr/lib/jvm/java-8-openjdk-amd64'
ENV['JAVA_HOME'] = base_path
ENV['PATH'] = "#{ENV['PATH']}:#{base_path}/bin"

# Install MongoDB
apt_package 'mongodb' do
  action :install
end

# Install Tomcat 7
include_recipe 'tomcat'

# Ensure Tomcat is running
service 'tomcat7' do
  action :start
end
