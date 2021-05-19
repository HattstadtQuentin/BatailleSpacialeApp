# BatailleSpacialeApp
Test Technique CAPCOD 2021

## Présentation
L’objectif de ce test est de mettre en avant les compétences techniques du candidat dans les
domaines suivants :
- Programmation orienté objet
- Langage C#
Le sujet sera à réaliser sous la forme d’une application console en C# réalisée à l’aide de Visual
Studio.
Vous aurez 1h pour réaliser le sujet. Ne pas finir le programme n’est pas éliminatoire tant qu’il y a du
contenu (en termes de code) à évaluer.
Conseil : lisez bien tout le sujet avant de commencer et choisissez à l’avance les fonctionnalités que
vous pensez pouvoir implémenter dans le temps impartie.
## Sujet
Il y a bien longtemps dans une galaxie lointaine, très lointaine ...
L’objectif de l’application est de simuler une bataille spatiale entre les rebelles et l’empire.
L’application prendra en entrée 2 paramètres : le nombre de soldats de l’empire et le nombre de
soldats rebelles. A l’issue de cette saisie la bataille pourra commencer.
Un soldat possède les caractéristiques suivantes :
- Santé (de 1000 –à 2000 points de vie)
- Dégâts infligés (de 100 à 500)
- Un nom ou un matricule
Le déroulement de la bataille :
- A chaque tour l’application choisi un soldat encore en jeu (avec sa santé > 0)
indépendamment de son camp
- Le soldat vise un ennemi encore en jeu et lui inflige des dégâts basé sur son attribut de
dégâts et modulé par un pourcentage en 0 et 100 aléatoire
- Le tour suivant commence
La bataille est terminée quand un des deux camps n’as plus de soldats viables.
Les fonctionnalités :
- Afficher pour chaque tour quel soldat attaque le quel et le résultat (si un soldat meurt, sinon
combien il lui reste de santé)
- Déterminer un favori avant le lancement de bataille et afficher si cela est respecté
- Afficher les héros de chaque équipe en fonction d’un score déterminé (calcul : santé + dégâts * 10). 
- Indiquer un message particulier quand un héros meurt
- Quand un rebelle attaque il doit dire « Pour la princesses Organa ! »
- Quand soldat de l’empire attaque il doit dire « Traitor ! »
