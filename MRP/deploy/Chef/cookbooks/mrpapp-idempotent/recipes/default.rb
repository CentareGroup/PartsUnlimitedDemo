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

# Install Tomcat 7
include_recipe 'tomcat'
