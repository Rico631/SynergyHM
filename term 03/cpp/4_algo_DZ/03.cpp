#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

void printV(const vector<int> &v)
{
  for (int e : v)
    cout << e << ' ';
  cout << endl;
}

void printCoins(vector<int> &coins, int sum)
{
  sort(coins.rbegin(), coins.rend());
  vector<int> usedCoins;
  int totalCount = 0;

  for (int coin : coins)
  {
    if (sum >= coin)
    {
      int count = sum / coin;
      sum %= coin;
      totalCount += count;

      for (int i = 0; i < count; i++)
        usedCoins.push_back(coin);
    }
  }

  if (sum == 0)
  {
    cout << "Минимальное количество монет: " << totalCount << endl;
    cout << "Монеты: ";
    printV(usedCoins);
  }
  else
    cout << "Не удалось набрать нужную сумму" << endl;
}

int main()
{
  vector<int> coins = {1, 2, 5, 10, 50};
  int sum = 27;

  cout << "Номиналы: ";
  printV(coins);
  cout << "Сумма: " << sum << endl;
  printCoins(coins, sum);

  return 0;
}
