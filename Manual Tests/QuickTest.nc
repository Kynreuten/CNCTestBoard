(1002)
(Manual Failings)
(Machine)
(  vendor: SainSmart)
(  model: Genmitsu 3018)
(  description: SainSmart 3018 Original)
(T1  D=0.25 CR=0 - ZMIN=-0.025 - flat end mill)
G90 G94
G17
G20
(When using Fusion 360 for Personal Use, the feedrate of)
(rapid moves is reduced to match the feedrate of cutting)
(moves, which can increase machining time. Unrestricted rapid)
(moves are available with a Fusion 360 Subscription.)
G0 Z0.5


(Bouncing up in speed by 1.18 each step.)
(Pauses between, pause length increases steadily)

(In We Go)
M9
T1
S20000 M3 (Spindle to Speed)

(Move to starting point)
G0 X0 Y0 G90
G0 Z0.3
G1 Z0.0 F4.72 (Feed for the last bit of Z in case there's material before we expect)
G91 (Change to relative move)

([00])
G1 Z-0.02 F4.72 (Move down by Step-down)
G1 X2 F4.72 (Move across X-axis)
G04 P500

([01])
G1 Z-0.02 (Down by Step-depth)
G1 X-2 F5.9
G04 P1000

([02])
G1 Z-0.02 (Down by Step-depth)
G1 X2 F7.08
G04 P1500

([03])
G1 Z-0.02 (Down by Step-depth)
G1 X-2 F8.26
G04 P2000

([04])
G1 Z-0.02 F4.72 (Move down by Step-down)
G1 X2 F9.44 (Move across X-axis)
G04 P2500

([05])
G1 Z-0.02 F4.72 (Move down by Step-down)
G1 X-2 F10.58 (Move across X-axis)
G04 P3000

([06])
G1 Z-0.02 F4.72 (Down by Step-depth)
G1 X2 F11.08 (Back to our starting point, but faster)
G04 P3500

([07])
G1 Z-0.02 F4.72 (Down by Step-depth)
G1 X2 F11.08 (Back to our starting point, but faster)
G04 P2000

([02])
G1 Z-0.02 F4.72 (Move down by Step-down)
G1 X2 F14.16 (Move across X-axis
G04 P2500

G1 Z-0.02 F4.72 (Down by Step-depth)
G1 X2 F16.52 (Back to our starting point, but faster)
G04 P3000


(TODO: Repeat passes at progressively faster speeds)

(TODO: When we are getting low on usable Z, move over as follows:)
(TODO: Move back to safe height, then Original X)
(TODO: Move on our Y-axis at least tool diameter in distance)


(TODO: Do same as first round, but more aggressive stepdown distance. Change Y at start. Or even flip X and Y?)

(DONE)
M30
