   M S B A M L   `   `   `  ����D  @Dusza_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Dusza_WPF	Dusza_WPF  5   	       Dusza_WPF.RelocateApps   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Dusza_WPF     5   	     � Title$  RelocateApps��5   	   $	��500��6   $	��800��6+   .��5	      � ���	#FF262627 � RowDefinitions 5
   
   �� 5      6   �� 5      6   �� 5      $
��0.4* �6   6,    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   .+�5   
   �� $��2��6   $��3q�5      
���   �._�5      �� -   5         ��Name$ lbTorlendoGepProgramok��5      
������$��20(�5      $��Center=�5      $��Center=�5      5      5       �� 5"   
   $��1��6   $	��Top=�5#      $��75��5$      $��Választható Gépek:��5%      $��20(�5&      $��Center=�5'      $��Center=�5(      5*      �� 5,   
   -   5         ��Name$ cbValasztottGep��51      $��1��5,      $	��200��5-      $��50��5.      $��0 50 0 0q�5/      $��20(�50      53      �� 55   
   $��1��6   $��1��56      $��3q�57      
���   �._�5:      �� -   5        $ lbValasztottGepProgramok��5;      
��� ���$��20(�5<      5>      5?      �� 5@   
   -   5         ��Name$ btnBefejezes��5@      $��2��5A      $��0��5B      $	��100��5C      $��50��5D      $��
Befejezés��5E         GeneralButton#���   5F      $��20(�5G      $��Center=�5H      ${�False��5I      5K      �� 5M   
   -   5        $ btnAthelyez��5M      $��2��5N      $��1��5O      $	��150��5P      $��50��5Q      $��Áthelyezés��5R      #���   5S      $��20(�5T      $��Center=�5U      5W      5X      5Y      