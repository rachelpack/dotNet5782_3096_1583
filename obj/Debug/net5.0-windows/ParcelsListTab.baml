   M S B A M L   `   `   `  ????=  9PL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLPL  5          PL.ParcelsListTab   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      "localclr-namespace:PL     5      .??5	      ?   ? ColumnDefinitions  5
   
   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6   ?? 5      $??* ?6    ? RowDefinitions 5   
   ?? 5      $	??10* ?6   ?? 5      $	??15* ?6   ?? 5      $	??52* ?6   .+?5   
   ]? $??0??6   $??4??6!   .+?5      ?? -   5         ??Name$ allTheParcels??5      $??All The Parcels??6,   6F   ?? 5       ?? Text$ 
Group list??6   ??5      ?? *GroupDescriptions+ ?? SelectedItem 5      ?? *GroupingSelected+??5      ?? 5      .R?5      ?? ??6$   ?? *PropertyName+5      5      ?? 5       -   5         ??Name$ statusSelector??5       ??5!      ?? *
Statuses+ 5"      ?? *ParcelModeSelected+6E   ?? 5#      -   5        $ prioritySelector??5#      ??5$      ?? *
Priorities+ 5%      ?? *PrioritySelected+6C   5'      ]? 5)   
   $??1??6   $??2??6!   $??2??61   .+?5*      ?? -   5        $ addANewParcel??5*      $??Add A New Parcel??6,   $??20q?6d   ?? 5+      -   5        $ closeParcel??5+      $??Close??6*   $??20q?6U   5,      ?? 5.   
   -   5         ??Name$ 
parcelList??5.      $??2??51      $??0??6    $??4??60   ??5/      ?? *ParcelListView+ ?? View 52      ?? 53      .P?54      ?? $??id??6%    ?? DisplayMemberBinding 55      ?? *Id+?? 56      $??senderId??6%    57      ?? *
SenderId+?? 58      $??
ReceivesId??6%    59      ?? *
ReceivesId+?? 5:      $??Status??6%    5;      ?? *Status+?? 5<      $??Priority??6%    5=      ?? *
Priority+5>      	 ?? 
GroupStyle	 5B      (  "System.Windows.Controls.GroupStyle  5C      
   ContainerStyle
 5D      ?? 5E       ?? 
TargetType" ??6    .?5F      ??  ?? Property ? ?? ?? Value 5G   "   ?? 5H   &   .R?5I   *   9? ??5J   .   ?? 5K   2    ?? Orientation$ 
Horizontal=?6=   .+?5L   6   ?? ??6@   ?? *Name+5M   3   .??5O   .   ?? 5P   +   5Q   '   5S      5T      5V      5X      5Y      5Z      