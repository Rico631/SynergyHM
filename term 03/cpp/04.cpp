#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

struct Lesson
{
  int start;
  int end;
};

bool compare(Lesson a, Lesson b)
{
  return a.end < b.end;
}

int maxLessons(vector<Lesson> &activities)
{
  sort(activities.begin(), activities.end(), compare);
  int count = 1;
  int lastEndTime = activities[0].end;
  for (int i = 1; i < activities.size(); i++)
  {
    if (activities[i].start >= lastEndTime)
    {
      count++;
      lastEndTime = activities[i].end;
    }
  }
  return count;
}

int main()
{
  vector<Lesson> activities = {{10, 20}, {12, 15}, {17, 19}, {5, 10}, {1, 4}};
  cout << "Занятий можно посетить: " << maxLessons(activities) << endl;
  return 0;
}
