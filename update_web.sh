#!/bin/bash
echo "==> Đang cập nhật WebClient..."
cd /var/game/GameHTTH
git pull
sudo chown -R www-data:www-data /var/game/GameHTTH/WebClient
sudo chmod -R 755 /var/game/GameHTTH/WebClient
sudo systemctl reload nginx
echo "==> CẬP NHẬT THÀNH CÔNG! WebClient đã được cập nhật trực tiếp."
