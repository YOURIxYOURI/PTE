   M S B A M L   `   `   `  ����E  AAdminPanel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:AdminPanel.ViewsAdminPanel.Views  5          AdminPanel.Views.Benefits   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:AdminPanel.Views     5      .��5	      � .+�5
   
   �� $��Admin Panel��6   $
��Left=�6&   $��	10,10,0,0q�6A   $	��Top=�6T   $��73��6l   $	��264��6x   $
��Bold"�6�   $��Calibri)�6�   $��48(�6�   �� 5   
   -   5           ButtonTemplate1#���   5      $��Uzytkownicy��6>   $
��Left=�6T   $��
279,10,0,0q�6o   $	��Top=�6�   $��60��6�   $	��210��6�   #��[" �6�   $��32(�6�   
  � Tag$  Users��6	   � Cursor$
 Hand��6  �� 5   
   -   5        $��	AdminName��5   �   $�� ��6   $
��Left=�6   $��
916,10,0,0q�66   $	��Top=�6J   $��24(�6b   $	��251��6p   $��Right=�6}   6�   �� 5   
   -   5        #��[" �5      #���   6L   $��Wyloguj się��6y   $
��Left=�6�   $��1064,36,0,0q�6�   $	��Top=�6�   $��18(�6�   $	��103��6�   ���	#FFED8181$
 Hand��6  �� 5   
   -   5        #���   5      $��Benefity��6>   $��
494,10,0,0q�6Q   $	��Top=�6e   $��60��6}   $	��182��6�   #��[" �6�   $��32(�6�   $
��Left=�6�   $  Benefits��6  $
 Hand��6  �� 5   
   -   5        #���   5      $��Administratorzy��6>   $
��Left=�6X   $��	681,9,0,0q�6s   $	��Top=�6�   $��60��6�   $	��241��6�   #��[" �6�   $��32(�6�   $  Admins��6  $
 Hand��6  &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
DGbenefits��5      $��0,127,814,8q�6'      AutoGenerateColumns$ False��6<      CanUserAddRows$ False��6X      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *ID+  5      $ Nazwa��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6M    64   �� *Name+6M     5      $ Data zakończenia��6%   $	 110 6[    6?   �� *	EndDate+6[   4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Edytuj��6)   	   CellTemplate	 5      �� 5      .R�5      �� -   5        $��Edytuj��5   %   #���   6C   ���	#FF3A7532���	#FFDEDEDE$
 Hand��6�   5      5        5      $ Usuń��6)   	 5      �� 5       .R�5!      �� -   5        $��Usuń��5!   %   #���   6C   ���	#FF3A7532���	#FFDEDEDE$
 Hand��6�   5"      5$      5&      �� 5(   
   -	   5        
 ��Name$
 NameForm��5(      $
��Left=�6$   $��747,102,0,0q�6?    �� TextWrapping$
 Wrap=�6T   $	��Top=�6h   $	��369��6�   $��33��6�   $��24(�6�   �� 5)   
   $��Nazwa��6   $
��Left=�6    $��672,102,0,0q�6;   $	��Top=�6P   $��33��6h   $��70��6t   $��20(�6   $��Right=�6�   �� 5*   
   -
   5        $
 DescriptionForm��5*      $
��Left=�6+   $��747,150,0,0q�6F   $	��Top=�6[   $	��369��6s   $	��139��6   $��24(�6�    �� AcceptsReturn$
 True��6�   �� 5+   
   $
��Opis��6   $
��Left=�6   $��647,150,0,0q�6:   $	��Top=�6O   $��49��6g   $��95��6s   $��20(�6~   $��Right=�6�   �� 5,   
   -   5        #���   5,      $��Dodaj ��6>   $
��Left=�6O   $��1012,401,0,0q�6j   $	��Top=�6�   $��50��6�   $	��104��6�   $��28(�6�   ���	#FF3A7532���	#FFDEDEDE$
 Hand��6�   �� 5-   
   -   5        $
 QRForm��5-      $
��Left=�6"   $��747,304,0,0q�6=   $
 Wrap=�6R   $	��Top=�6f   $	��369��6~   $��33��6�   $��24(�6�   �� 5.   
   $��Zawartość kodu QR��6   $
��Left=�6,   $��552,304,0,0q�6G   $	��Top=�6\   $��33��6t   $	��190��6�   $��20(�6�   $��Right=�6�   (  "System.Windows.Controls.DatePicker  5/   
   -   5          Name$ EndDateForm��5/      $
��Left=�6*   $��747,355,0,0q�6E   $	��Top=�6Z   $	��369��6r   $��33��6~   $��20(�6�   �� 50   
   $��Data Zakończenia��6   $
��Left=�6+   $��573,355,0,0q�6F   $	��Top=�6[   $��33��6s   $	��169��6   $��20(�6�   $��Right=�6�   �� 51   
   -   5         ��Name$ 	InfoLabel��51      $
��Left=�6$   $��712,485,0,0q�6?   $	��Top=�6T   $	��409��6l   $��62��6x   $��20(�6�   ���	#FFFB4444$��Right=�6�   �� 52   
   -   5        $
 	SearchBar��52      $
��Left=�6%   $��6,88,0,0q�6@   $
 Wrap=�6R   $	��Top=�6f   $	��272��6~   $��34��6�   $��24(�6�   �� 53   
   -   5        #���   53      $��Szukaj��6>   $
��Left=�6O   $��
283,88,0,0q�6j   $	��Top=�6~   $��34��6�   $��72��6�   $��22(�6�   ���	#FF3A7532���	#FFDEDEDE$
 Hand��6�   54      55      