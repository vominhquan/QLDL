# QLDL
- Project Quản lý đại lý
- WPF - Mô hình 3 lớp
- Project tạo bằng Visual Studio 2013
- CSDL SQL Server 2012 
- Dùng Entity Framework để kết nối CSDL

# Start Project
1. Clone project
2. restore csdl = file QLDL.bak
3. Trong folder DataAccess (nếu ko có thì tạo) tạo Model EF mới theo hướng dẫn: https://youtu.be/K4x6eoG7hwY?t=4m14s (đọc tiếp lưu ý)
4. Lưu ý: - tên new item là 'QLDLModel'; - chỗ "Save connection settings in App.config" đặt tên là: 'QLDLEntities'; - lấy hết tables, views, store procedures and functions
5. Cầu trời cho nó lên
6. lưu ý về sau: mỗi khi csdl thay đổi như thêm cột, sửa bảng,... thì xóa file .edmx trong DataAccess và tạo lại, Visual Studio ko update dc CSDL (do nó ngu, ko phải tại mình)

