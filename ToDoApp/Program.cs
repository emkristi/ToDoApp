﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace ToDoApp
{
    public class Task
    {
        public string Task_To_Do { get; set; }
        public int Task_ID { get; set; }

        public Task(string Task_To_Do, int Task_ID)
        {
            this.Task_ID = Task_ID;
            this.Task_To_Do = Task_To_Do;
        }
    }

    public class ToDoList
    {
        // Method for updating the list by reading from File and adding to list
        public void UpdateList(List<Task> list, string filename)
        {
            string line;
            StreamReader sr = new StreamReader(filename);
            while ((line = sr.ReadLine()) != null)
            {
                string readId = line.Substring(0, line.IndexOf('/'));
                int ID = Int32.Parse(readId);
                string readTask = line.Substring(line.IndexOf('/') + 1);
                list.Add(new Task(readTask, ID));
            }
            sr.Close();
        }

        // Writes the list into a text file as strings
        public void WriteListToFile(List<Task> list, string filename)
        {
            string toFile;
            StreamWriter sw = new StreamWriter(filename);
            foreach (Task t in list)
            {
                toFile = t.Task_ID + "/" + t.Task_To_Do;
                sw.WriteLine(toFile);
            }
            sw.Flush();
            sw.Close();

        }

        // Reads from file. File is used to save state. 
        public void PrintListFromFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string readId = line.Substring(0, line.IndexOf('/'));
                string readTask = line.Substring(line.IndexOf('/') + 1);
                Console.WriteLine("#" + readId + " " + readTask);
            }
            sr.Close();
        }

        // Method for removing task that is done given the ID.
        public void DoTask(List<Task> list, string input)
        {
            string findTaskId = input.Remove(0, input.IndexOf('#') + 1);
            int readTaskId = Int32.Parse(findTaskId);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Task_ID == readTaskId)
                {
                    Console.WriteLine("Completed #" + list[i].Task_ID + " " + list[i].Task_To_Do);
                    list.RemoveAll(e => e.Task_ID == readTaskId);
                }
            }
        }

        // Method that assigns an ID, used for adding a new task
        public int AssignId(List<Task> list)
        {
            int id = 1;
            int maxId = int.MinValue;

            foreach (Task t in list)
            {
                if (t.Task_ID > maxId)
                {
                    maxId = t.Task_ID;
                }
                id = maxId + 1;
            }
            return id;
        }

        // Method that checks the rest of the string after the word Add. Returns the task written.
        // Used for adding a new task
        public string CheckAddString(string input){
            string taskRead = input;
            if (taskRead.Contains("\"")){
                Match match = Regex.Match(taskRead, "\"([^\"]*)\"");
                if (match.Success){
                    taskRead = match.Groups[1].Value;
                }
            }else{
                taskRead = input.Substring(input.IndexOf(' ') + 1);
            }
            return taskRead;
        }



        public static void Main(string[] args)
        {
            Console.WriteLine("Choose command: Add \"example task\" or Add example task | Do #tasknumber | Print ");

            string filename = @"/Users/eline/Projects/ToDoApp/ToDoApp/TextFile.txt";
            List<Task> list = new List<Task>();
            ToDoList thelist = new ToDoList();

            thelist.UpdateList(list, filename);


            while (true)
            {
                string input = Console.ReadLine();
                string commandVerb = "";

                if (input.Contains(" "))
                {
                    commandVerb = input.Substring(0, input.IndexOf(' '));
                }

                if (commandVerb == "Add")
                {
                    string taskRead = thelist.CheckAddString(input);
                    int taskid = thelist.AssignId(list);
                    list.Add(new Task(taskRead, taskid));
                    thelist.WriteListToFile(list, filename);

                    Console.WriteLine("#" + taskid + " " + taskRead);

                }
                else if (commandVerb == "Do")
                {
                    thelist.DoTask(list, input);
                    thelist.WriteListToFile(list, filename);

                }
                else if (input == "Print")
                {
                    thelist.PrintListFromFile(filename);
                }else{
                    Console.WriteLine("No such command.");
                }
            }
        }
    }
}
