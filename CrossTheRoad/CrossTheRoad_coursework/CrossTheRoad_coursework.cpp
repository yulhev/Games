#include <deque>
#include <iostream>
#include <conio.h>
#include <time.h>
#include <vector>

using namespace std;
class cPlayer
{
public:
	int x, y;
	cPlayer(int width) { x = width / 2; y = 0; }
};
class cLane
{
private:
	deque<bool>cars;
	bool right;
public:
	cLane(int widht)
	{
		for (int i = 0; i < widht; i++)
			cars.push_front(true);
		right = rand() % 2;
	}
	void  move()      //moving of the cars
	{
		if (right)
		{
			if (rand() % 10 == 1)
				cars.push_front(true);
			else
				cars.push_front(false);
			cars.pop_back();
		}
		else
		{
			if (rand() % 10 == 1)
				cars.push_back(true);
			else
				cars.push_back(false);
			cars.pop_front();
		}
	}
	bool checkpos(int pos) { return cars[pos]; }
	void changedirection() { right!=right; }
};

class cGame
{
private:
	bool quit;
	int numoflanes;
	int width;
	int score;
	cPlayer* player;
	vector <cLane*> map;

public:
	cGame(int w = 20, int h = 20)
	{
		numoflanes = h;
		width = w;
		quit = false;
		for (int i = 0; i < numoflanes; i++)
			map.push_back(new cLane(width));
		player = new cPlayer(width);

	}
	~cGame()        
	{
		delete player;
		for (int i = 0; i < map.size(); i++)
		{
			cLane* current = map.back();
			map.pop_back();
			delete current;
		}
	}
	void draw()
	{
		system("cls");
		for (int i = 0; i < numoflanes; i++)
		{
			for (int j = 0; j < width; j++)
			{
				if (i == 0 && (j == 0 || j == width - 1))
					cout << "S";
				if (i == numoflanes - 1 && (j == 0 || j == width - 1))
					cout << "F";
				if (map[i]->checkpos(j) && i != 0 && i != numoflanes - 1)
					cout << "#";
				else if (player->x == j && player->y == i)
					cout << "V";
				else
					cout << " ";
			}
			cout << endl;
		}
		cout << "Score: " << score << endl;
	}
	void input()  //navigation
	{
		if (_kbhit())
		{
			char current = _getch();
			if (current == 'a')
				player->x--;
			if (current == 'd')
				player->x++;
			if (current == 'w')
				player->y--;
			if (current == 's')
				player->y++;
			if (current == 'q')
				quit = true;


		}
	}
	void logic()
	{
		for (int i = 1; i < numoflanes - 1; i++)
		{
			if (rand() % 10 == 1)
				map[i]->move();
			if (map[i]->checkpos(player->x) && player->y == i)
				quit = true;
		}
		if (player->y == numoflanes - 1)
		{
			score++;
			player->y = 0;
			cout << "\x07"; //after the end, it returns to the first line  
			map[rand() % numoflanes]->changedirection();
		}
	}
	void run()
	{
		while (!quit)
		{
			input();
			draw();
			logic();

		}
	}
};

int main()
{
	int r, c;
	cout << "Enter dimension of the board (two numbers):";
	cin >> r >> c;
	srand(time(NULL));
	cGame game(r,c);
	game.run();
	cout << "Game over!((" << endl;
	getchar();
	return 0;
}