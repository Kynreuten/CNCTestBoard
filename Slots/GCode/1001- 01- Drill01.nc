(1001)
(Round 001)
(Machine)
(  vendor: SainSmart)
(  model: Genmitsu 3018)
(  description: SainSmart 3018 Original)
(T6  D=0.0787 CR=0 TAPER=118deg - ZMIN=-0.05 - drill)
G90 G94
G17
G20
G28 G91 Z0
G90

(Drill1)
T6
S1000 M3
G54
G0 X0.1991 Y0.2014
Z0.6
Z0.2
Z0.175
G1 Z-0.05 F7.09
G0 Z0.2
Z0.6
G28 G91 Z0
G90
G28 G91 X0 Y0
G90
M5
M30
