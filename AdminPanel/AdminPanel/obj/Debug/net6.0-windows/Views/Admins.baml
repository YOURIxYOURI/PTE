   M S B A M L   `   `   `  ����E  AAdminPanel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:AdminPanel.ViewsAdminPanel.Views  5          AdminPanel.Views.Admins   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:AdminPanel.Views     5      .��5	      � .+�5
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
 Hand��6  &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DGadmins��5      $��0,134,674,0q�6%      AutoGenerateColumns$ False��6:      CanUserAddRows$ False��6V      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *ID+  5      $ Imię��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6Q    63   �� *	FirstName+6Q     5      $ Nazwisko��6%   $	 100 6T    67   �� *
LastName+6T     5      $ Email��6%   $	 150 6N    64   �� *Email+6N   4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $ Czy Nadrzędny��6)    6@   �� *IfMain+5      4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Usuń��6)   	   CellTemplate	 5      �� 5      .R�5      �� -   5        $��Usuń��5   %   #���   6C   ���	#FF3A7532���	#FFDEDEDE$
 Hand��6�   5      5      5!      �� 5"   
   -   5        
 ��Name$
 	FnameForm��5"      $
��Left=�6%   $��747,102,0,0q�6@    �� TextWrapping$
 Wrap=�6U   $	��Top=�6i   $	��369��6�   $��33��6�   $��24(�6�   �� 5#   
   $��Imię��6   $
��Left=�6   $��672,102,0,0q�6:   $	��Top=�6O   $��33��6g   $��70��6s   $��20(�6~   $��Right=�6�   �� 5$   
   -	   5        $
 	LnameForm��5$      $
��Left=�6%   $��747,150,0,0q�6@   $	��Top=�6U   $	��369��6m   $��33��6y   $��24(�6�   �� 5%   
   $��Nazwisko��6   $
��Left=�6#   $��647,150,0,0q�6>   $	��Top=�6S   $��33��6k   $��95��6w   $��20(�6�   $��Right=�6�   �� 5&   
   -
   5        #���   5&      $��Dodaj ��6>   $
��Left=�6O   $��1012,366,0,0q�6j   $	��Top=�6�   $��50��6�   $	��104��6�   $��28(�6�   ���	#FF3A7532���	#FFDEDEDE$
 Hand��6�   �� 5'   
   -   5        $
 	EmailForm��5'      $
��Left=�6%   $��747,201,0,0q�6@   $
 Wrap=�6U   $	��Top=�6i   $	��369��6�   $��33��6�   $��24(�6�   �� 5(   
   $��E-Mail��6   $
��Left=�6!   $��672,201,0,0q�6<   $	��Top=�6Q   $��33��6i   $��70��6u   $��20(�6�   $��Right=�6�   G� 5)   
   -   5         G�Name$ PassForm��5)      $
��Left=�6(   $��747,252,0,0q�6C   $	��Top=�6X   $	��369��6p   $��33��6|   $��24(�6�   �� 5*   
   $��Hasło��6   $
��Left=�6    $��664,252,0,0q�6;   $	��Top=�6P   $��33��6h   $��78��6t   $��20(�6   $��Right=�6�   G� 5+   
   -   5        $ PassConfForm��5+      $
��Left=�6,   $��747,302,0,0q�6G   $	��Top=�6\   $	��369��6t   $��33��6�   $��24(�6�   �� 5,   
   $��Potwierdź Hasło��6   $
��Left=�6*   $��585,302,0,0q�6E   $	��Top=�6Z   $��33��6r   $	��157��6~   $��20(�6�   $��Right=�6�   �� 5-   
   -   5         ��Name$ 	InfoLabel��5-      $
��Left=�6$   $��686,440,0,0q�6?   $	��Top=�6T   $	��430��6l   $��62��6x   $��20(�6�   ���	#FFFB4444$��Right=�6�   �� 5.   
   -   5        $
 	SearchBar��5.      $
��Left=�6%   $��	15,92,0,0q�6@   $
 Wrap=�6S   $	��Top=�6g   $	��386��6   $��33��6�   $��24(�6�   �� 5/   
   -   5        #���   5/      $��Szukaj��6>   $
��Left=�6O   $��
406,92,0,0q�6j   $	��Top=�6~   $��34��6�   $��72��6�   $��22(�6�   ���	#FF3A7532���	#FFDEDEDE$
 Hand��6�   50      51      