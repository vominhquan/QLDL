   M S B A M L   `   `   `  ����?  ;QLDL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Applications.ComponentsApplications.Components  5   	   &     QLDL.Presentation.DanhSachLoaiDL   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   3_%clr-namespace:Applications.Components     5   	     J� SnapsToDevicePixels  .  � Title$" Danh sách loại đại lý��5   	   $	��514��6'   $	��811��64   .��5      ]� -   5        $
��Main��5         DockPanelScale#��[  6   .+�5   
   ��  
 Shadow#��[ 5	      $	��Top=�5
      .+�5      ��  �� Padding$ 10q�6   ._�5      ]� .+�5      ��  �� Orientation$ 
Horizontal=�5      $
��Left=�5      .+�5      $   Applications.Components.Button  $   Applications.Components.Button   Click$
 Back��5         Text$ 
Quay lại��5         Icon$	 ��5      "   Applications.Components.Line  5        5      $	 Add��5      $	 ��5      $ Thêm loại đại lý��5      5      5      �� 5      $ 
Horizontal=�5      $��Right=�5      .+�5      #   Applications.Components.Input     Height$ 50Q�5      $	��250��5         Border$ 0q�5       	   Label$
	 Tên��5!      
   
LabelWidth$
 60Q�5"         Value 5#      ��  �� Path$ 
LocTheoTen� �� UpdateSourceTrigger$ PropertyChanged=�  5'        5(      $ Refresh��5*      $	 ��5)      $ 
Làm mới��5+        5,        5-      $
 Save��5/      $	 ��5.      $
 Lưu��50      51      �� 52      53      54      55      �� 56   
   -   5        $��ListViewDanhSachDaiLy��57      $��20q�58       �� SelectionMode$ Single=�59        ListViewItem#��[ 5:      ��5;      �� $ DanhSachLoaiDL� �� View 5<      �� 5=      .P�5>      �� $��Tên loại đại lý��6%    �� Width$	 250��6>    �� CellTemplate 5?      �� 5@      .R�5A   "   �� $��-6, 0q�5B   %   $	��250��5C   %   $ 
Horizontal=�5D   %   .+�5E   &     $	��249��5F   )      
Background$ Transparent��5G   )      
Foreground$ White��5H   )      Overlay$ 	#dd227447��5I   )   $ 50Q�5J   )      Align$
 Left��5L   )    5K   )   �� *	TENLOAI+5L   )     5M   &   $��1��6-   ���#21734667   5N   #   5O      5Q      �� 5R      $��Số nợ tối đa��6%   $	 250��6;    5S      �� 5T      .R�5U   "   �� $��-6, 0q�5V   %   $	��250��5W   %   $ 
Horizontal=�5X   %   .+�5Y   &     $	��200��5Z   )   $ Transparent��5[   )   $ White��5\   )   $ 	#dd227447��5]   )   $ 50Q�5^   )   $
 Left��5b   )    5_   )   �� *	SONOTOIDA+  
DecimalToD �� 	Converter# [ 5b   )     5c   &    RemoveSoNoToiDa5d   )   $��50��5f   )   $��50��5g   )   $	 ��5h   )   
 � Tag 5e   )   �� 5h   )   5i   #   5j      5l      5m      5o      5p      5q      