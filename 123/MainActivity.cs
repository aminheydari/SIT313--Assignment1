using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.AppCompat;
using Android.Views;
using Android.Content;
using System;
using Application.Helper;
using System.Collections.Generic;

namespace Application
{
    [Activity(Label = "123", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        EditText taskEditText;
        DbHelper dbHelper;
        CustomAdapter adapter;
        ListView firstTask;



        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_item,menu);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Resource.Id.action_add:
                    taskEditText = new EditText(this);
                    Android.Support.V7.App.AlertDialog dialog = new Android.Support.V7.App.AlertDialog.Builder(this)
                        .SetTitle("Add New Task")
                        .SetMessage("What do you want to do next?")
                        .SetView(taskEditText)
                        .SetPositiveButton("Add", OkAction)
                        .SetNegativeButton("Cancel", CancelAction)
                        .Create();
                    dialog.Show();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void CancelAction(object sender, DialogClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            string task = taskEditText.Text;
            dbHelper.InsertNewTask(task);
            LoadTaskList();
        }

        public void LoadTaskList()
        {
            List<string> taskList = dbHelper.getTaskList();
            adapter = new CustomAdapter(this, taskList, dbHelper);
            firstTask.Adapter = adapter;


        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView (Resource.Layout.Main);

            dbHelper = new DbHelper(this);
            firstTask = FindViewById<ListView>(Resource.Id.firstTask);


            // load data 

            LoadTaskList();
        }
    }
}

