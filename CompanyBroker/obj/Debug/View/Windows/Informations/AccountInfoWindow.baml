   M S B A M L   `   `   `  ����  CompanyBroker?    9CompanyBroker.View.Windows.Informations.AccountInfoWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          �Name$   TheUpdateAccountInfoWindow��5   	    � Title$+ %Company broker : account informations��5	   	   $	��380��67   $	��400��6D    � 
ResizeMode$ NoResize=�6Q    � WindowStartupLocation$ CenterScreen=�6g    � DataContext 5   	   �� *AccountInfo+   Locator �� Source# [  .��5
      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      $��30 �6   6*   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      $��35 �6   6*   .+�5    
   �� $��0��6   $��0��6   $��3��6-   $��Center=�6A   .��6^   Account informations6t   �� 5$   
   $��1��6   $��Center=�6   $��Center=�6:   .��6V   Account name6d   �� 5%   
   $��2��6   $��Center=�6   $��Center=�6:   .��6U   Email6\   �� 5&   
   $��3��6   $��Center=�6   $��Center=�6:   .��6U   Reset password6e   �� 5'   
   $��4��6   $��Center=�6   $��Center=�6:   .��6U   Active6]   �� 5*   
   $��1��6   $��1��6   $��Center=�6/   {�.  ��6J   �� *AccountName+ �� Mode$ TwoWay=�6�   �� 5+   
   $��2��6   $��1��6   $��Center=�6/   ��6J   �� *AccountEmail+$ TwoWay=�6w   G� 5,   
   -   5        	 G�Name$	 PassWordField��5,   N   $��3��6   $��1��6#   $��Center=�63   6g   �� 5-   
   $��4��6   $��1��6    
 d� 	IsChecked
 60   �� *
IsActive+$ TwoWay=�6^   �� 50   
   $��0��6   $��9��6!   $��Center=�6.   $	��120��6I   ��6U   �� *UpdateCommand+��6w   ��  �� ElementName$ PassWordField�� � 	Resources 51       �� 
TargetType �� Property �� Value��%_   '��       �� 52      " ��6   .�53      ��  �@��CornerRadius$ 10��65   54      55      .��6    Update57      �� 5:   
   $��2��6   $��9��6!   $��Center=�6.   $	��120��6I   ��6U   �� *CloseWindowCommand+ 5;      ��%_   '��       �� 5<      " ��6   .�5=      ��  �@��CornerRadius$ 10��65   5>      5?      .��6    Close5A      5B      5C      