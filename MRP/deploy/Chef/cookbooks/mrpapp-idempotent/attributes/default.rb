default['java']['install_flavor'] = 'openjdk'
default['java']['jdk_version'] = '7'

default['tomcat']['run_base_instance'] = false
default['tomcat']['base_version'] = 7
# default['tomcat']['base_instance'] = 'tomcat7'
# default['tomcat']['packages'] = ["tomcat#{node['tomcat']['base_version']}"]
# default['tomcat']['deploy_manager_packages'] = ["tomcat#{node['tomcat']['base_version']}-admin"]
# default['tomcat']['user'] = "tomcat#{node['tomcat']['base_version']}"
# default['tomcat']['group'] = "tomcat#{node['tomcat']['base_version']}"
# default['tomcat']['home'] = "/usr/share/tomcat#{node['tomcat']['base_version']}"
# default['tomcat']['base'] = "/var/lib/tomcat#{node['tomcat']['base_version']}"
# default['tomcat']['config_dir'] = "/etc/tomcat#{node['tomcat']['base_version']}"
# default['tomcat']['log_dir'] = "/var/log/tomcat#{node['tomcat']['base_version']}"
# default['tomcat']['tmp_dir'] = "/tmp/tomcat#{node['tomcat']['base_version']}-tmp"
# default['tomcat']['work_dir'] = "/var/cache/tomcat#{node['tomcat']['base_version']}"
# default['tomcat']['context_dir'] = "#{node['tomcat']['config_dir']}/Catalina/localhost"
# default['tomcat']['webapp_dir'] = "/var/lib/tomcat#{node['tomcat']['base_version']}/webapps"
# default['tomcat']['keytool'] = 'keytool'
# default['tomcat']['lib_dir'] = "#{node['tomcat']['home']}/lib"
# default['tomcat']['endorsed_dir'] = "#{node['tomcat']['lib_dir']}/endorsed"

# Setup MRP Tomcat instance
default['tomcat']['instances'] = {
  'mrp' => {
    'port' => 9080,
    'shutdown_port' => 8007
  },
  'orderservice' => {
    'port' => 8080,
    'shutdown_port' => 8006
  }
}
