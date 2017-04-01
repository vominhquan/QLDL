# QLDL
- Project Quản lý đại lý
- WPF - Mô hình 3 lớp
- Project tạo bằng Visual Studio 2013
- CSDL SQL Server 2012 
- Dùng Entity Framework để kết nối CSDL

# Start Project
1. Clone project
2. Restore csdl = file QLDL.bak
3. Xóa file App.config
4. Trong folder DataAccess (nếu ko có thì tạo (tạo = VS) - ngang hàng với folder BusinessLogic, Presentation) tạo Model EF mới theo hướng dẫn: https://youtu.be/K4x6eoG7hwY?t=4m14s (đọc tiếp lưu ý)
5. Lưu ý: - tên new item là 'QLDLModel'; - chỗ "Save connection settings in App.config" đặt tên là: 'QLDLEntities'; - lấy hết tables, views, store procedures and functions
6. Cầu trời cho nó chạy mượt

# Lưu ý về lớp Model:
- Mỗi khi csdl thay đổi như thêm cột, sửa bảng,... thì xóa file .edmx trong DataAccess (xóa = VS) và tạo lại như trên, do Visual Studio đôi khi ko update dc CSDL, tạo lại thì 100% dc (do nó ngu, ko phải tại mình)
- Để dùng Inotify phải thêm 2 dòng code trong file model cần dùng (khi xóa file .edmx phải thêm lại từng class T.T) VD: ![alt tag](http://i68.tinypic.com/t5hvea.png)
