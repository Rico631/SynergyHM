#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

int maxProdSeq(vector<int> &nums, int k)
{
  int n = nums.size();
  if (n < k)
  {
    cout << "Длина последовательности больше массива" << endl;
    return -1;
  }
  vector<int> maxProd(k + 1, -1e9);
  vector<int> minProd(k + 1, 1e9);
  maxProd[0] = 1;
  minProd[0] = 1;
  for (int i = 0; i < n; ++i)
  {
    for (int j = k; j > 0; --j)
    {
      if (maxProd[j - 1] != -1e9)
      {
        maxProd[j] = max(maxProd[j], maxProd[j - 1] * nums[i]);
        minProd[j] = min(minProd[j], minProd[j - 1] * nums[i]);
      }
    }
  }
  return maxProd[k];
}

int main()
{
  vector<int> nums = {1, -2, 3, -4, -1, 2};
  int k = 3;
  cout << "Массив чисел: ";
  for (int n : nums)
    cout << n << ' ';
  cout << endl;
  cout << "Длина подпоследовательности: " << k << endl;
  cout << "Результат: " << maxProdSeq(nums, k) << endl;

  return 0;
}
