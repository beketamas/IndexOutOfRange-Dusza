   M S B A M L   `   `   `  ����D  @Dusza_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Dusza_WPF	Dusza_WPF  5      "    Dusza_WPF.ManageApplications   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:Dusza_WPF     5        N� Title$  ManageApplications��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      $��4* �6   6*   �� 5      6   .+�5   
   �� $��0��6   $��0��5      $��Klaszter Programok��5      $��25(�5      $��Center=�5      $��Center=�5      $	��250��5      $
��Left=�5      $��20 0 0 0q�5      5      �� 5    
   $��0��6   $��1��5!      $��Programpéldányok��5"      $��25(�5#      $��Center=�5$      $��Center=�5%      $��Right=�5&      $��0 0 20 0q�5'      $	��250��5(      5)      �� 5+   
   -   5         ��Name$ lblError��52      $��0��5+      $��20(�5,      $��2��5-      $��Center=�5.      $	��480��5/      $��50��50      
���  ��$��Center=�53      $��Center=�54      56      �� 58   
   -   5        $ lbKlaszterProgramok��59      $��1��58      
�������$��20(�5;      $��Center=�5=      5>      �� 5?   
   -   5        $ lbProgrampeldanyok��5A      $��1��5?      $��1��5@      
�������$��20(�5C      $��Center=�5E      5G      �� 5H   
   -   5         ��Name$ btnProgramLeallitas��5P      $��2��5H      $��2��5I      $	��190��5J      $��50��5K      $��18(�5L      $��Program Leállíása��5M      $
��Left=�5N      $��
0, 20,0, 0q�5O      ${�False��5Q         GeneralButton#���   5R      5T      �� 5V   
   -   5        $ btnProgramSzerkeztese��5\      $��2��5V      $	��205��5W      $��50��5X      $��18(�5Y      $��Program Szerkeztése��5Z      $
��Left=�5[      ${�False��5]      $��
270,20,0,0q�6#   #���   5^      �� 5`   
   -   5        $ btnProgramokSzetosztasa��5f   	   $��2��5`      $	��220��5a   	   $��50��5b   	   $��18(�5c   	   $��Programok Szétosztása��5d   	   $
��Left=�5e   	   $
{�True��5g   	   $��1��6   $��	40,20,0,0q�5h   	   #���   5i   	   �� 5k   
   -   5        $ btnPeldanyLeallitasa��5s      $��2��5k      $��2��5l      $	��180��5m      $��50��5n      $��18(�5o      $��Példány Leállítása��5p      $��	0 20 20 0q�5q      $��Right=�5r      ${�False��5t      #���   5u      5v      5w      5x      