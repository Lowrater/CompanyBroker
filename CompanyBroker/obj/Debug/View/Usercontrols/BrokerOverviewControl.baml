   M S B A M L   `   `   `  ����  CompanyBroker;    5CompanyBroker.View.Usercontrols.BrokerOverviewControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � DataContext  5      �� *BrokerOverview+   Locator �� Source# [  .��5	      �  � ColumnDefinitions 5   
   �� 5      6!    � RowDefinitions 5   
   �� 5      $��30 �6   6*   �� 5      6   .+�5   
   � $��0��6    5      �� 5      6%   �� 5      6%   �� 5      6%   �� 5      6%   .+�5       �� $��0��6   $��3��6&    �� TextAlignment$
 Left=�6:   $��Center=�6O   ��6q   �� *SearchField+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.��6�    J� InputBindings 5!      �� 5"      
 �� Key$ Return��6I   	 �� Command	 6!   �� *ExecuteQueryCommand+6X   .��5#   )   5$      �� 5&      $��3��6   ��6%   �� *ExecuteQueryCommand+.��6M   Search6U   5'      &   System.Windows.Controls.DataGrid  5*   
   -   5        
  Name$
 ResourceList��5*      $��1��6)      CanUserAddRows .     CanUserResizeColumns .     
IsReadOnly . $ �� IsSynchronizedWithCurrentItem$
 TrueY�6�   ��66   �� *MainRersourceList+ �� SelectedItem 6�   �� *ResourceRowSelection+$ OneWayToSource=�   Columns 5+      4  .System.Windows.Controls.DataGridTemplateColumn  5,      ,  &System.Windows.Controls.DataGridColumn   CanUserResize .     CanUserReorder .     CellTemplate 5-      �� 5.      .R�5/      ��  �� Orientation$ 
Horizontal=�6)     6C   �� *BrokerOverview+# [  .+�50   "   �� $��50��6)   
��������6L   �� *BuyResourceCommand+��6s   �� *SelectedItem+ �� ElementName$ ResourceList��.��6�   Buy6�   �� 51   "   $��50��6)   
�������.��6M   	Favorit6V   �� 52   "   $��50��6)   
����ح���6K   �� *OpenResourceInfoWindowCommand+.��6}   Info6�   53      54      56      58      59      5:      