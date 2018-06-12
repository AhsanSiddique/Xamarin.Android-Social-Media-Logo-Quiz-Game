using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace LogoQuizGame.Adapter
{
    public class GridViewSuggestAdapter : BaseAdapter
    {
        private List<string> suggestSource;
        private Context context;
        private MainActivity mainActivity;

        public GridViewSuggestAdapter(List<string> suggestSource, Context context, MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
            this.context = context;
            this.suggestSource = suggestSource;
        }
        public override int Count
        {
            get { return suggestSource.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Button button;
            if (convertView == null)
            {
                if (suggestSource[position].Equals("null"))
                {
                    button = new Button(context);
                    button.LayoutParameters = new GridView.LayoutParams(85, 85);
                    button.SetPadding(8, 8, 8, 8);
                    button.SetBackgroundColor(Color.DarkGray);
                }
                else
                {
                    button = new Button(context);
                    button.LayoutParameters = new GridView.LayoutParams(85, 85);
                    button.SetPadding(8, 8, 8, 8);
                    button.SetBackgroundColor(Color.DarkGray);
                    button.SetTextColor(Color.Yellow);
                    button.Text = Convert.ToString(suggestSource[position]);
                    button.Click += delegate 
                    {
                        //Convert Char Array to String
                        string answer = new string(mainActivity.answer);
                        if (answer.Contains(suggestSource[position]))
                        {
                            char compare = suggestSource[position][0]; // get character from string
                                                                       //Similar charAt(index in java
                            for (int i = 0; i < answer.Length; i++)
                            {
                                if (compare == answer[i])
                                    Common.Common.user_submit_answer[i] = compare;
                            }
                            //Update UI
                            GridViewAnswerAdapter answerAdapter = new GridViewAnswerAdapter(Common.Common.user_submit_answer, context);
                            mainActivity.gvAnswer.Adapter = answerAdapter;
                            answerAdapter.NotifyDataSetChanged();

                            //Remove character from suggest source
                            mainActivity.suggestSource[position] = "null";
                            mainActivity.suggestAdapter = new GridViewSuggestAdapter(mainActivity.suggestSource, context, mainActivity);
                            mainActivity.gvSuggest.Adapter = mainActivity.suggestAdapter;
                            mainActivity.suggestAdapter.NotifyDataSetChanged();
                        }
                        else
                        {
                            //Remove character from suggest source
                            mainActivity.suggestSource[position] = "null";
                            mainActivity.suggestAdapter = new GridViewSuggestAdapter(mainActivity.suggestSource, context, mainActivity);
                            mainActivity.gvSuggest.Adapter = mainActivity.suggestAdapter;
                            mainActivity.suggestAdapter.NotifyDataSetChanged();
                        }
                    };
                }
            }
            else
                button = (Button)convertView;
            return button;
        }
    }
}