   M S B A M L   `   `   `  ����@  <TRIAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TRIALTRIAL  5   	       TRIAL.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:TRIAL     5   	   -   5          �Name$  
mainwindow��5   	   #��Z��5	   	   #��Z��5       � Title$ 
MainWindow��5   	    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ 
Style.xaml?�6%   5      %    5      .��5      � $��
0,0,0,-448q�6    � ColumnDefinitions 5   
   �� 5      .+�5   
   �� -   5         ��Name$ ivnod��5      $
��Left=�6$   $	��737��6@   $	��737��6R   $	��230��6_   $	��220��6n   $	��Top=�6}   .+�5      �� 
���   ��� 5      $&�� /Trial;component/Image/newab.jpg��6   $
��left=�6>   $��61��6Y   $��0,0,84,0q�6e   $��90��6w   6�   �� 5       -   5        $ txtblk��5       $��34��6)   
�������   	stackBlue#��[  6J   5!      R� 5"      -   5         R�Name$ treeview��5"      $	��248��6*   
�������$	��2.5q�6P   $��0,0,2,0q�6f   $��Stretch=�5#      $��Stretch=�5$       %� IsVirtualizing .  	 %� VirtualizationMode$	 Standard=�5'      
����+����5%      ��  5(      
 �� Color �� 
TargetType �� Property �� Value �� Triggers ^� 
Conditions �� Binding �� RelativeSource �� Path �� Value �� AncestorType��%�  (��       5)   &   * SystemColors.HighlightBrushKey+)(�� "     5+   &   *$"SystemColors.HighlightTextBrushKey+)(�� I     5-   &   *1/SystemColors.InactiveSelectionHighlightBrushKey+)(�� p     5/   &   *53SystemColors.InactiveSelectionHighlightTextBrushKey+)'Q� �     �� 5)      $

 Gray��5*      �� 5+      $
 	LightGray��5,      �� 5-      $
 	LightGray��5.      �� 5/      $
 	LightGray��50      �� 51      " Q�6   .�52      ��  � �� 53      �� 54   "   .R�55   &   �� ._�56   *   �� ��64   �� ��57   *   �� 58   .   " ��64   .�59   2   ��  � ��$ 1q�6T   �� 5:   2    � ��
 ���� 6P    5;   2   ^� 5<   6    5=   :   �� 5>   >    6H   ��  �� *Self+$ IsMouseOver� True5?   F   �� 5@   >    6H   ��  �� $ TreeViewItemL�$ 
IsSelected�	 False5A   E   .0�5C   :   ��  � ��
 ��  �6W   �� 5D   :    � ��
 �����6X   5E   7   5G   /   5I   '   5J   #   5L      5M      5N      5O      �� 5Q      -   5        $ 	mainpanel��5Q      #��[  6,   $	��391��6O   6_   5S       � 5T   
   -   5          �Name$ 
_mainFrame��5T      $��	232,0,0,0q�6&   &   System.Windows.Controls.DataGrid  5U   
   -   5        $��g1��5V   9   "   VerticalScrollBarVisibility$
 Auto=�5U   ;   $   HorizontalScrollBarVisibility$
 Auto=�5V      
�������   AutoGenerateColumns .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5X   D      	RowHeight$ 30Q�5Y      $��15(�6#      RowBackground
 �����   AlternatingRowBackground
 �����   GridLinesVisibility$	 All=�5Z         ColumnHeaderHeight$ 50Q�6,   
���   �$��3q�5[   (   $	��Top=�6<   $��
232,63,0,0q�5\      $	��674��6(    � DataContext 5U   _   ��   �� ElementName$  
mainwindow����5W      �� !   Columns! 5`      0  *System.Windows.Controls.DataGridTextColumn  5c        CenterGridHeaderStyle,  &System.Windows.Controls.DataGridColumn"   HeaderStyle#" [ 6k   #   Header$# Broker Code��6�   -   5        
BrokerCode��$   Width$	$ 100 5c   �   1  +System.Windows.Controls.DataGridBoundColumn%   Binding% 6%   �� *
BrokerCode+& �� UpdateSourceTrigger$& PropertyChanged=�'   HeaderTemplate' 5d      �� 5e      .R�5f      �� ( �� Orientation$( Vertical=�6)   .+�5g   "   �� $��Center=�6(   $��Center=�6C   $��Center=�6g   $��Broker Code��6�   �� 5h   "   ) ��Name$) NameTextBox��6*   -	   5        $��2q�5h   @   $	��100��6T   ��6�   �� *Filter+$  
mainwindow��$& PropertyChanged=�* �� Mode$* TwoWay=�5i      5j      5n        5p      #" [ 6i   -
   5        
BrokerName��$	$ 100 5p   �   % 6%   �� *
BrokerName+$& PropertyChanged=�# 5q      �� 5r      $( Vertical=�6%   .+�5s      �� $��Center=�6$   $��Center=�6?   $��Center=�6c   $��Broker Name��6�   �� 5t      -   5        $) NameBox��5t   &   $��2q�67   $	��100��6K   6Z   5u      5w        5y      #" [ 6h   -   5        RTTMCode��$	$ 100 5y   �   % 6%   �� *
RTTMCode+$& PropertyChanged=�# 5z      �� 5{      $( Vertical=�6%   .+�5|      �� $��Center=�6$   $��Center=�6?   $��Center=�6c   $��	RTTM Code��6�   �� 5}      -   5        $
) RTTM��5}   &   $��2q�64   $	��100��6H   6W   5~      5�        5�      #" [ 6d   $	$ 100 6�   % 6%   �� *DTCNo+$& PropertyChanged=�# 5�      �� 5�      $( Vertical=�6%   .+�5�      �� $��Center=�6$   $��Center=�6?   $��Center=�6c   $��DTC No��6�   �� 5�      -   5        $	) DTC��5�   &   $��2q�63   $	��100��6G   6V   5�      5�        5�      #" [ 6d   $	$ 100 6�   % 6%   �� *Notes+$& PropertyChanged=�# 5�      �� 5�      $( Vertical=�6%   .+�5�      �� $��Center=�6$   $��Center=�6?   $��Center=�6c   $��Notes��6�   �� 5�      -   5        $) Notes��5�   &   $��2q�65   $	��100��6I   6X   5�      5�        5�      #" [ 6c   $	$ 100 6�   % 6%   �� *Name+$& PropertyChanged=�# 5�      �� 5�      $( Vertical=�6%   .+�5�      �� $��Center=�6$   $��Center=�6?   $��Center=�6c   $
��Name��6�   �� 5�      -   5        $
) Name��5�   &   $��2q�64   $	��100��6H   6W   5�      5�        5�      #" [ 6d   $	$ 100 6�   % 6%   �� *Phone+$& PropertyChanged=�# 5�      �� 5�      $( Vertical=�6%   .+�5�      �� $��Center=�6$   $��Center=�6?   $��Center=�6c   $��Phone��6�   �� 5�      -   5        $) Phone��5�   &   $��2q�65   $	��100��6I   6X   5�      5�        5�      #" [ 6b   $	$ 100 6�   % 6%   �� *Fax+$& PropertyChanged=�# 5�      �� 5�      $( Vertical=�6%   .+�5�      �� $��Center=�6$   $��Center=�6?   $��Center=�6c   $��Broker Name��6�   �� 5�      -   5        $	) Fax��5�   &   $��2q�63   $	��100��6G   6V   5�      5�        5�      #" [ 6e   $	$ 100 6�   % 6%   �� *Email+$& PropertyChanged=�# 5�      �� 5�      $( Vertical=�6%   .+�5�      �� $��Center=�6$   $��Center=�6?   $��Center=�6c   $��Broker Name��6�   �� 5�      -   5        $) Email��5�   &   $��2q�65   $	��100��6I   6X   5�      5�      5�      � 5�   
   $
��Left=�6   $	��100��6*   $	��Top=�67   $	��186��6O   5�      5�      