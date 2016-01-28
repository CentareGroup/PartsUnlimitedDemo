override['tomcat']['run_base_instance'] = false
override['tomcat']['base_version'] = 7

# Setup MRP Tomcat instance
override['tomcat']['instances'] = {
  'mrp' => {
    'port' => 9080,
    'shutdown_port' => 8007
  },
  'orderservice' => {
    'port' => 8080,
    'shutdown_port' => 8006
  }
}
