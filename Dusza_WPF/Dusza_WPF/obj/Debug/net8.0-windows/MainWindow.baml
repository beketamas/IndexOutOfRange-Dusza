   M S B A M L   `   `   `  ����D  @Dusza_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Dusza_WPF	Dusza_WPF  5   	       Dusza_WPF.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Dusza_WPF     5   	     � Title$  
MainWindow��5      $	��770��6    $
��1150��6-   
������  � AllowsTransparency$
 True��6S    � WindowStyle$
 None=�6m    � 
ResizeMode$ NoResize=�6�    � WindowStartupLocation$ CenterScreen=�6�    � 	Resources 5	         Button.MouseOver.Background �� Color  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border  Button.Disabled.Foreground  WindowStyle �� 
TargetType ��Name	 J� SnapsToDevicePixels
 ��Name �� Triggers P� Property P� Value �� 
TargetName �� Value �� Property��%�  &	        &	 #     &	 F     &	 i     &	 �     &	 �     &	 �     &	 �     7   7  7  7  7  7  7  �� 5
   
   $ 	#FFBEE6FD��6>   �� 5   
   $ 	#FF3C7FB1��6:   �� 5   
   $ 	#FFC4E5F6��6<   �� 5   
   $ 	#FF2C628B��68   �� 5   
   $ 	#FFF4F4F4��6=   �� 5   
   $ 	#FFADB2B5��69   �� 5   
   $ 	#FF838383��6=   �� 5   
   " ��6.   .R�5      �� $ border��6   #��z��6%   #��z��6O   #��z��6{   $
	 true��6�   ._�5      �� $
 contentPresenter��6#   $|�False��6=   #��z��6O   #��z��6�   $
��True��6�   #	 z	 6�   #��z��6  5       5      P� 5       �@��IsDefaulted$
 true��68   . �5      �� $ border��64   # � ��6H    � ��5      P� 5       �@J�IsMouseOver$
 true��61   . �5      �� $ border��63   8    � ��6G   �� 5      $ border��64   8   � ��6H   5      P� 5       � ��$
 true��6/   . �5      �� $ border��63   8   � ��6G   �� 5      $ border��64   8   � ��6H   5       P� 5!       � {�$ false��6/   . �5"      �� $ border��63   8   � ��6G   �� 5#      $ border��64   8   � ��6H   �� 5$      $ contentPresenter��6?   8   � ��6]   5%      5'      5(      .��5,      �� $��20q�6   ._�5-   
   �� -   5        $ window��5-      $��20q�6!   ���	#FF262627 �� CornerRadius$	 1.5��6D    J� Effect 5.      3  -System.Windows.Media.Effects.DropShadowEffect  5/         	Direction$ 0Q�6#      
BlurRadius$ 12Q�61      Opacity$ 1Q�6A      ShadowDepth$ 0Q�6M   ._�51      �� .+�52      �� -   5        $ navbar��52      $
��1000��6)   $��60��66   $��20q�6B     
borderback#���  6N   $ 5��6s    53        54         Color$ Black��6+   $ 12Q�69   $ .7Q�6I   $ 0Q�6V   ._�56      ��  �� Orientation$ 
Horizontal=�6!   $
��1000��6:   $��50��6G   .+�5<      �� -   5         ��Name$ btnThemeToggle��5<   !   $��30��69   $��30��6D   $	��☀��6P   $��20(�6\    	 ThemeChanger#��� 	 6j   $��30,0,0,0q�6�   6�   �� 5=      -   5        $ 	btnEleres��5=   !   $��Elérés��64    
 	NavButton#��� 
 6E   $��22��6i   $
��Bold"�6u   $��15(�6�   $��30,0,0,0q�6�   6�   �� 5>      -   5        $ 
btnManager��5>   !   ${�False��65    J� Opacity$	 0.5Q�6G   $��Klaszter Kezelő��6U   #��� 
 6o   $��22��6�   $
��Bold"�6�   $��15(�6�   $��30,0,0,0q�6�   6�   �� 5?      -   5        $ btnAddComputer��5?   !   ${�False��69   $	 0.5Q�6K   $��Gép hozzáadása��6Y   #��� 
 6r   $��22��6�   $
��Bold"�6�   $��15(�6�   $��30,0,0,0q�6�   6�   �� 5@      -   5        $ btnManageApplications��5@   !   ${�False��6@   $	 0.5Q�6R   $��Programok kezelése��6`   #��� 
 6}   $��22��6�   $
��Bold"�6�   $��15(�6�   $��30,0,0,0q�6�   6�   �� 5A      -   5        $ btnStartApplications��5A   !   ${�False��6?   $	 0.5Q�6Q   $��Program Indítása��6_   #��� 
 6z   $��22��6�   $
��Bold"�6�   $��15(�6�   $��30,0,0,0q�6�   6�   �� 5B      -	   5        $ btnDeleteComputer��5B   !   ${�False��6<   $	 0.5Q�6N   $��Számítógép törlése��6\   #��� 
 6y   $��22��6�   $
��Bold"�6�   $��15(�6�   $��30,0,0,0q�6�   6�   �� 5E      -
   5        $ ShutdownButton��5E   !   $��X��69     ShutdownButton#���  6E   $��33��6n   $��33��6y   $��20(�6�   
�������
���  ��$��Center=�6�   $��Center=�6�   $��0q�6�   $��35,0,0,0q�6	  6  5F      5G       � 5H      -   5          �Name$ 	Container��5H        � NavigationUIVisibility$ Hidden=�6+   $	��575��6K   $
��1000��6X   5I      5J      5L      5M      