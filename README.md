Description

..........................................................................................................................................................................................................................

This .NET console application implements an advanced version of the classic Rock-Paper-Scissors game. It supports an arbitrary odd number of moves, allowing for more complex gameplay such as Rock-Paper-Scissors-Lizard-Spock. The game ensures fair play through the use of cryptographic techniques, generating a random move and calculating its HMAC (Hash-based Message Authentication Code) before the user makes their move. This way, the integrity of the computer's move is verifiable.


Features

...........................................................................................................................................................................................................................

    Flexible Move Sets: Accepts an odd number (≥ 3) of non-repeating strings as moves.
    Cryptographic Integrity: Uses a cryptographically secure random key to generate an HMAC for the computer's move.
    User Interaction: Provides a menu for the user to choose their move, view a help table, or exit the game.
    Help Table: Displays a dynamically generated help table showing which moves win, lose, or draw against each other.
    Separate Classes: The project is structured with separate classes for table generation, rule definitions, key generation, and HMAC functions, adhering to clean code principles.


How to Play

...........................................................................................................................................................................................................................

    Start the Game: Run the game with command line arguments specifying the moves.
    View HMAC: The game displays the HMAC of the computer's move.
    Make Your Move: Choose your move from the provided menu.
    View Results: See the result of the game, the computer's move, and the HMAC key to verify the integrity of the computer's move.
    Help Table: Type ? to view a detailed help table explaining the game rules.


Example Usage

...........................................................................................................................................................................................................................

dotnet run -- rock Spock paper lizard scissors



Output:

...........................................................................................................................................................................................................................

HMAC: 9ED68097B2D5D9A968E85BD7094C75D00F96680DC43CDD6918168A8F50DE8507
Available moves:
1 - rock
2 - Spock
3 - paper
4 - lizard
5 - scissors
0 - exit
? - help
Enter your move: 3
Your move: paper
Computer move: rock
You win!
HMAC key: BD9BE48334BB9C5EC263953DA54727F707E95544739FCE7359C267E734E380A2


...........................................................................................................................................................................................................................
This project showcases the use of .NET for building interactive console applications with a focus on security and fair play through cryptographic methods.
...........................................................................................................................................................................................................................

