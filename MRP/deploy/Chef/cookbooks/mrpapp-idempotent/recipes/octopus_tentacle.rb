# Setup and install Mono
apt_repository 'mono-xamarin' do
  uri 'http://download.mono-project.com/repo/debian'
  components ['main']
  distribution 'wheezy'
  key '3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF'
  keyserver 'keyserver.ubuntu.com'
  action :add
  deb_src true
end

apt_package 'mono-complete' do
  action :install
end

apt_package 'tar' do
  action :install
end
