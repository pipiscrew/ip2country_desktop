# ip2country_desktop  

The application made in bounds of **apache logging**.. The main purpose is to paste the contents of apache **access.log** and reveal to grid the  
* origin **country**
* **IP range** for the **organization** owns the IP

nevertheless user can paste **just plain IPs** and retrieve the same data.  

This opens the opportunity to create **black list rules** (`/etc/nftables.conf`) for **nftables** (successor of iptables).  

To export the black list rules, the list must contains **unique IPs** or **IP ranges**. The **unique IP ranges** can be filtered when `CTRL pressed` and the `unique IP` button is clicked.  

User is able to drag'n'drop **access.log** to grid for quick load.. Sort by column, delete rows etc.. Inspired by [http Logs Viewer](https://www.apacheviewer.com/).  

> The application requires the **GeoLite2 Free Geolocation Data** to be near the application, the so called file  **GeoLite2-Country.mmdb**, this can downloaded by [here](https://github.com/P3TERX/GeoLite.mmdb) or [here](https://github.com/wp-statistics/GeoLite2-Country) or [here](https://github.com/LOVECHEN/GeoLite.mmdb) or 
[here](https://dev.maxmind.com/geoip/geolite2-free-geolocation-data).  

![shot](https://github.com/user-attachments/assets/2e4ae356-2be2-4ab1-bb52-c2c92ccb8d37)

&nbsp;

## This project uses the following 3rd-party dependencies :
* [MaxMind.Db](https://www.nuget.org/packages/MaxMind.Db/3.0.0)
* [MaxMind.GeoIP2](https://www.nuget.org/packages/MaxMind.GeoIP2/4.1.0)
* Database and Contents Copyright (c) [MaxMind](https://www.maxmind.com/) Inc.  

---

## This project is no longer maintained
Copyright (c) 2024 [PipisCrew](http://pipiscrew.com)  

Licensed under the [MIT license](http://www.opensource.org/licenses/mit-license.php).