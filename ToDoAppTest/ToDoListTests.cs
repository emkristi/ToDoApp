using System;
using System.Collections.Generic;
using Xunit;

namespace ToDoAppTest
{
    public class ToDoListTests
    {

        [Fact]
        public void DoTask_TaskDoesNotExist_DoesNotRemoveAnyTasksFromList(){
            var toDoList = new ToDoApp.ToDoList();
            var task = new ToDoApp.Task("Task no1", 1);
            var task2 = new ToDoApp.Task("Task no2", 2);
            var task3 = new ToDoApp.Task("Task no3", 3);
            var aList = new List<ToDoApp.Task> { task, task2, task3 };

            toDoList.DoTask(aList, "Do #4");

            var expectedList = new List<ToDoApp.Task> { task, task2, task3 };
            Assert.Equal(expectedList, aList);
        }

        [Fact]
        public void DoTask_TaskExists_RemovesTaskFromList(){
            var toDoList = new ToDoApp.ToDoList();
            var task = new ToDoApp.Task("Task no1", 1);
            var task2 = new ToDoApp.Task("Task no2", 2);
            var task3 = new ToDoApp.Task("Task no3", 3);
            var aList = new List<ToDoApp.Task> { task, task2, task3 };

            toDoList.DoTask(aList, "Do #1");

            var expectedList = new List<ToDoApp.Task> { task2, task3 };
            Assert.Equal(expectedList, aList);
        }

        [Fact]
        public void AssignId_ListIsEmpty_Returns1()
        {
            var toDoList = new ToDoApp.ToDoList();
            var aList = new List<ToDoApp.Task> {};

            var expected_ID = 1;
            var ID = toDoList.AssignId(aList);

            Assert.Equal(expected_ID, ID);
        }

        [Fact]
        public void AssignId_IdExists_ReturnsIntBiggerThanTheBiggestInList(){
            var toDoList = new ToDoApp.ToDoList();
            var task = new ToDoApp.Task("Task no1", 1);
            var task2 = new ToDoApp.Task("Task no2", 2);
            var task5 = new ToDoApp.Task("Task no5", 5);
            var task6 = new ToDoApp.Task("Task no6", 6);
            var aList = new List<ToDoApp.Task> { task, task2, task5, task6 };

            var expected_ID = 7;
            var ID = toDoList.AssignId(aList);

            Assert.Equal(expected_ID, ID);
        }

        [Fact]
        public void CheckString_InputContainsQuotationMark_ReturnTask(){
            var toDoList = new ToDoApp.ToDoList();
            string aString = "Add" + " " + "\"a task\"";
            string expectedString = "a task";
            
            string actualString = toDoList.CheckAddString(aString);
            Assert.Equal(expectedString, actualString);
        }

        [Fact]
        public void CheckString_InputDoesNotContainQuotationMark_ReturnTask(){
            var toDoList = new ToDoApp.ToDoList();
            string aString = "Add" + " " + "a task";
            string expectedString = "a task";

            string actualString = toDoList.CheckAddString(aString);
            Assert.Equal(expectedString, actualString);
        }

    }
}

