List<string> inputs = new List<string>();
using (StreamReader reader = new StreamReader(args[0]))
{
    while (!reader.EndOfStream)
    {
        inputs.Add(reader.ReadLine());
    }
}

part1();
part2();

void part1()
{
    int runTotal = 0;


    /*foreach (var item in inputs)
    {*/
    int rows = inputs.Count;
    int cols = inputs[0].Length;

    bool[,] valid_spaces = new bool[rows, cols];
    // INIT SPACES
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            valid_spaces[i, j] = false;
        }
    }

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            char c = inputs[i][j];
            if ((c >= '0' && c <= '9') || c == '.')
            {
                //num or not symbol
            }
            else
            {
                //symbol
                valid_spaces[i, j] = true;
                if (i > 0 && j > 0)
                    valid_spaces[i - 1, j - 1] = true;
                if (i > 0)
                    valid_spaces[i - 1, j] = true;
                if (i > 0 && j < cols - 1)
                    valid_spaces[i - 1, j + 1] = true;

                if (i < rows - 1 && j > 0)
                    valid_spaces[i + 1, j - 1] = true;
                if (i < rows - 1)
                    valid_spaces[i + 1, j] = true;
                if (i < rows - 1 && j < cols - 1)
                    valid_spaces[i + 1, j + 1] = true;

                if (j > 0)
                    valid_spaces[i, j - 1] = true;
                if (j < cols - 1)
                    valid_spaces[i, j + 1] = true;
            }
        }
    }

    string digit_string = "";
    bool count_it = false;
    for (int i = 0; i < rows; i++)
    {
        if (count_it)
            runTotal += Convert.ToInt32(digit_string);
        //reset
        count_it = false;
        digit_string = "";
        for (int j = 0; j < cols; j++)
        {
            char c = inputs[i][j];
            if (c >= '0' && c <= '9')
            {
                digit_string = digit_string + c;
                if (valid_spaces[i, j])
                {
                    count_it = true;
                }
            }
            else
            {
                if (count_it)
                    runTotal += Convert.ToInt32(digit_string);
                //reset
                count_it = false;
                digit_string = "";
            }
        }
    }

    //}
    Console.WriteLine(runTotal);
}

void part2()
{
    int runTotal = 0;


    Dictionary<int, List<int>> gear_numbers = new();

    /*foreach (var item in inputs)
    {*/
    int rows = inputs.Count;
    int cols = inputs[0].Length;

    string[,] valid_spaces = new string[rows, cols];
    // INIT SPACES
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            valid_spaces[i, j] = "";
        }
    }

    int gearNum = 0;

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            char c = inputs[i][j];
            if (c == '*')
            {
                gearNum++;
                //symbol
                valid_spaces[i, j] += "_" + Convert.ToString(gearNum);
                if (i > 0 && j > 0)
                    valid_spaces[i - 1, j - 1] = "_" + Convert.ToString(gearNum);
                if (i > 0)
                    valid_spaces[i - 1, j] = "_" + Convert.ToString(gearNum);
                if (i > 0 && j < cols - 1)
                    valid_spaces[i - 1, j + 1] = "_" + Convert.ToString(gearNum);

                if (i < rows - 1 && j > 0)
                    valid_spaces[i + 1, j - 1] = "_" + Convert.ToString(gearNum);
                if (i < rows - 1)
                    valid_spaces[i + 1, j] = "_" + Convert.ToString(gearNum);
                if (i < rows - 1 && j < cols - 1)
                    valid_spaces[i + 1, j + 1] = "_" + Convert.ToString(gearNum);

                if (j > 0)
                    valid_spaces[i, j - 1] = "_" + Convert.ToString(gearNum);
                if (j < cols - 1)
                    valid_spaces[i, j + 1] = "_" + Convert.ToString(gearNum);
            }
        }
    }

    string digit_string = "";
    bool count_it = false;
    HashSet<int> count_it_where = new();
    for (int i = 0; i < rows; i++)
    {
        if (count_it_where.Count() > 0)
        {
            foreach (var gear_id in count_it_where)
            {
                if (gear_numbers.ContainsKey(gear_id))
                {
                    gear_numbers[gear_id].Add(Convert.ToInt32(digit_string));
                }
                else
                {
                    gear_numbers[gear_id] = new List<int>();
                    gear_numbers[gear_id].Add(Convert.ToInt32(digit_string));
                }

            }
        }
        //reset
        count_it_where.Clear();
        digit_string = "";
        for (int j = 0; j < cols; j++)
        {
            char c = inputs[i][j];
            if (c >= '0' && c <= '9')
            {
                digit_string = digit_string + c;
                if (valid_spaces[i, j] != "")
                {
                    foreach (var gearId in valid_spaces[i, j].Substring(1).Split('_'))
                        count_it_where.Add(Convert.ToInt32(gearId));
                }
            }
            else
            {
                if (count_it_where.Count() > 0)
                {
                    foreach (var gear_id in count_it_where)
                    {
                        if (gear_numbers.ContainsKey(gear_id))
                        {
                            gear_numbers[gear_id].Add(Convert.ToInt32(digit_string));
                        }
                        else
                        {
                            gear_numbers[gear_id] = new List<int>();
                            gear_numbers[gear_id].Add(Convert.ToInt32(digit_string));
                        }

                    }
                }
                //reset
                count_it_where.Clear();
                digit_string = "";
            }
        }
    }
    //Console.WriteLine($"gear_count{gearNum}");

    foreach (var key in gear_numbers.Keys)
    {
        if (gear_numbers[key].Count == 2)
        {
            runTotal += gear_numbers[key][0] * gear_numbers[key][1];
        }
    }

    //}
    Console.WriteLine(runTotal);
}