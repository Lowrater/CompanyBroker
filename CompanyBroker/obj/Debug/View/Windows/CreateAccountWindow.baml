   M S B A M L   `   `   `  ����  CompanyBroker4    .CompanyBroker.View.Windows.CreateAccountWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          �Name$  TheCreateAccountWindow��5   	    � Title$&  Company broker: Account creation��5	   	   $	��380��62   $	��400��6?    � WindowStartupLocation$ CenterScreen=�6K    � 
ResizeMode$ NoResize=�6p    � DataContext 5   	   �� *CreateAccount+   Locator �� Source# [  .��5
      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      $��35 �6   6*   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      $��35 �6   6*   .+�5    
   �� $��0��6   $��0��6   $��3��6-   $��Center=�6A   .��6^   Account creation6p   �� 5#   
   $��1��6   $��Center=�6   $��Center=�6:   .��6U   New company?6c   �� 5$   
   $��2��6   $��Center=�6   $��Center=�6:   .��6U   Company name6c   �� 5%   
   $��4��6   $��Center=�6   $��Center=�6:   .��6U   Account name6c   �� 5&   
   $��5��6   $��Center=�6   $��Center=�6:   .��6U   	Company6^   �� 5'   
   $��6��6   $��Center=�6   $��Center=�6:   .��6U   Email6\   �� 5(   
   $��7��6   $��Center=�6   $��Center=�6:   .��6U   
Password6_   �� 5,   
   $��1��6   $��1��6    $��Center=�60    d� 	IsChecked 6K   �� *NewCompanyBool+{�6p   �� *
IsLoggedIn+6�   �� 5-   
   $��2��6   $��1��6   $��Center=�6/   ��6J   �� *CompanyName+{�6g   �� *CompanyNameBool+6�   �� 50   
   $��4��6   $��1��6   $��Center=�6/   ��6J   �� *AccountName+6i   �� 51   
   $��5��6   $��1��6    $��Center=�61   ��6L   �� *CompanyList+	 �� Mode$	 TwoWay=�
 �� SelectedIndex
 6}   �� *CompanyChoice+$	 TwoWay=�{�6�   �� *CompanyDropDownBool+6�   �� 52   
   $��6��6   $��1��6   $��Center=�6/   ��6J   �� *AccountEmail+6j   G� 53   
   -   5         G�Name$ PassWordField��53   N   $��7��6   $��1��6#   $��Center=�63   6g   �� 56   
   $��1��6   $��9��6!   $��Center=�6.   $	��120��6I   ��6U   �� *CreateCommand+��6w   ��  �� ElementName$ PassWordField�� � 	Resources 57       �� 
TargetType �� Property �� Value��%_   '��       �� 58      " ��6   .�59      ��  �@��CornerRadius$ 10��65   5:      5;      .��6    Create5<      5=      5>      