# Cryptography.CardanGrille
A .NET library for encoding messages via Cardan Grille algorithm

Welcome everyone!

This is my first attempt to start a non-tutorial repository on GitHub. 
Occasionally, I also started a cryptography course not long ago, that's why I'm glad to 
introduce you my variant of encoding with Cardan Grille algorithm (see more here https://en.wikipedia.org/wiki/Cardan_grille)

I'm not working on it already, but it still needs some updates:
1) It's not set to work with 5*5 grille, where there's only one slot for the 7th element. These leads to data loose if you're
using the existing code
2) I suppose it's a placeholder field that should be add to the Key class, so that there won't be any mistakes, connected
with the situations, when the placeholder has been changed before the message decoded.

I would be glad for any of your commits, comments, advice and ideas! Thank you for helping me to develop my skills
