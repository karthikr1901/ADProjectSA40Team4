package com.example.student.adprojectsa40team4;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Fragment;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.FragmentManager;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;


public class MainActivity extends ActionBarActivity
        implements NavigationDrawerFragment.NavigationDrawerCallbacks ,
        RequisitionFormsFragment.OnFragmentInteractionListener,
        CollectionPointsInfoFragment.OnFragmentInteractionListener,
        DisbursementListFragment.OnFragmentInteractionListener,
        ApproveAdjustmentFragment.OnFragmentInteractionListener,
		InventoryStatusFragment.OnFragmentInteractionListener,
		ARRFragment.OnFragmentInteractionListener,
        ARRItemFragment.OnFragmentInteractionListener,
        DelegateAuthorityFragment.OnFragmentInteractionListener,
        CancellationOfAuthority.OnFragmentInteractionListener,
        AppointRepresentativeFragment.OnFragmentInteractionListener,
        POReceiveFragment.OnFragmentInteractionListener{

    /**
     * Fragment managing the behaviors, interactions and presentation of the navigation drawer.
     */
    private NavigationDrawerFragment mNavigationDrawerFragment;

    /**
     * Used to store the last screen title. For use in {@link #restoreActionBar()}.
     */
    private CharSequence mTitle;
    Intent intent;
    String role = "";
    Context context;
    Integer result,flag=0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        //context = getA.getApplicationContext();


        mNavigationDrawerFragment = (NavigationDrawerFragment)
                getSupportFragmentManager().findFragmentById(R.id.navigation_drawer);
        mTitle = getTitle();

        // Set up the drawer.
        mNavigationDrawerFragment.setUp(
                R.id.navigation_drawer,
                (DrawerLayout) findViewById(R.id.drawer_layout));

        if (LogInModel.empRole == 6 )
        {
            flag = 1;
            new AsyncTask<Void, Void, Integer>() {
                @Override
                protected Integer doInBackground(Void... params) {
                     result = LogInModel.GetLowLevelStockQty();
                    return  result;
                }

                @Override
                protected void onPostExecute(Integer result) {
                    if (result > 0) {
                        AlertDialog.Builder builder = new AlertDialog.Builder(MainActivity.this);
                        builder.setMessage(result + " items are under stock level. ")
                                .setCancelable(false)
                                .setPositiveButton("Check", new DialogInterface.OnClickListener() {
                                    public void onClick(DialogInterface dialog, int id) {
                                        Fragment fragment = new InventoryStatusFragment();
                                        android.app.FragmentManager fragmentManager = getFragmentManager();
                                        fragmentManager.beginTransaction()
                                                .replace(R.id.container, fragment)
                                                .commit();
                                    }
                                })
                                .setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
                                    public void onClick(DialogInterface dialog, int id) {
                                        dialog.cancel();
                                    }
                                });
                        AlertDialog alert = builder.create();
                        alert.show();

                    } else {

                    }
                }
            }.execute();
        }

        Log.w("Error", " on On Create Method !!!!!!!!");
    }

    @Override
    public void onNavigationDrawerItemSelected(int position) {
        // update the main content by replacing fragments

            android.app.FragmentManager fragmentManager = getFragmentManager();
            Fragment fragment=new Fragment();
            if (LogInModel.empRole == 6)
            {
                switch (position){
                    //Home
                    case 0:
                        fragment = new RequisitionFormsFragment();
                        mTitle = getString(R.string.STMTitle1);
                        break;

                    case 1:
                        fragment = new InventoryStatusFragment();
                        mTitle = getString(R.string.STMTitle2);
                        break;

                    case 2:
                        fragment = new CollectionPointsInfoFragment();
                        mTitle = getString(R.string.STMTitle3);
                        break;

//                    case 3:
//                        fragment = new POReceiveFragment();
//                        mTitle = getString(R.string.STMTitle4);

                }
            }
        else  if (LogInModel.empRole == 3)
            {
                switch (position){
                    //Home
                    case 0:
                        fragment = new ARRFragment();
                        mTitle = getString(R.string.DPHTitle1);
                        break;

                    case 1:
                        fragment = new DelegateAuthorityFragment();
                        mTitle = getString(R.string.DPHTitle2);
                        break;

                    case 2:
                        fragment = new CancellationOfAuthority();
                        mTitle = getString(R.string.DPHTitle3);
                        break;

                    case 3:
                        fragment = new AppointRepresentativeFragment();
                        mTitle = getString(R.string.DPHTitle4);
                        break;

                }
            }
            else  if (LogInModel.empRole == 7)
            {
                switch (position){
                    case 0:
                        fragment = new ApproveAdjustmentFragment();
                        mTitle = getString(R.string.SupervisorTitle1);
                        break;
                }
            }
            else  if (LogInModel.empRole == 5)
            {
                switch (position){
                    case 0:
                        fragment = new ApproveAdjustmentFragment();
                        mTitle = getString(R.string.SupervisorTitle1);
                        break;
                }
            }

            fragmentManager.beginTransaction()
                    .replace(R.id.container, fragment)
                    .commit();
    }

    public void onSectionAttached(int number) {
        if (LogInModel.empRole == 6){
            switch (number) {
                case 1:
                    mTitle = getString(R.string.STMTitle1);
                    break;
                case 2:
                    mTitle = getString(R.string.STMTitle2);
                    break;
                case 3:
                    mTitle = getString(R.string.STMTitle3);
                    break;
//                case 4:
//                    mTitle = getString(R.string.STMTitle4);
             }
        }
        else  if (LogInModel.empRole == 3){
            switch (number) {
                case 1:
                    mTitle = getString(R.string.DPHTitle1);
                    break;
                case 2:
                    mTitle = getString(R.string.DPHTitle2);
                    break;
                case 3:
                    mTitle = getString(R.string.DPHTitle3);
                    break;
                case 4:
                    mTitle = getString(R.string.DPHTitle4);
                    break;
            }
        }
        else if (LogInModel.empRole == 7){
            switch (number) {
                case 1:
                    mTitle = getString(R.string.SupervisorTitle1);
                    break;
            }
        }
        else if (LogInModel.empRole == 5){
            switch (number) {
                case 1:
                    mTitle = getString(R.string.SupervisorTitle1);
                    break;
            }
        }
    }

    public void restoreActionBar() {
        ActionBar actionBar = getSupportActionBar();
        actionBar.setNavigationMode(ActionBar.NAVIGATION_MODE_STANDARD);
        actionBar.setDisplayShowTitleEnabled(true);
        actionBar.setTitle(mTitle);
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        if (!mNavigationDrawerFragment.isDrawerOpen()) {
            // Only show items in the action bar relevant to this screen
            // if the drawer is not showing. Otherwise, let the drawer
            // decide what to show in the action bar.
            getMenuInflater().inflate(R.menu.main, menu);
            restoreActionBar();
            return true;
        }

        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_logout) {
            FragmentManager fragmentManager = getSupportFragmentManager();
            // this will clear the back stack and displays no animation on the screen
            fragmentManager.popBackStackImmediate();
            finish();
        }

        return super.onOptionsItemSelected(item);
    }


            @Override
            public void onFragmentInteraction(Uri uri) {

            }

    @Override
    public void onFragmentInteraction(String id) {

    }

    /**
     * A placeholder fragment containing a simple view.
     */
    public static class PlaceholderFragment extends Fragment {
        /**
         * The fragment argument representing the section number for this
         * fragment.
         */
        private static final String ARG_SECTION_NUMBER = "section_number";

        /**
         * Returns a new instance of this fragment for the given section
         * number.
         */
        public static PlaceholderFragment newInstance(int sectionNumber) {
            PlaceholderFragment fragment = new PlaceholderFragment();
            Bundle args = new Bundle();
            args.putInt(ARG_SECTION_NUMBER, sectionNumber);
            fragment.setArguments(args);
            return fragment;
        }

        public PlaceholderFragment() {
        }

        @Override
        public View onCreateView(LayoutInflater inflater, ViewGroup container,
                                 Bundle savedInstanceState) {
            View rootView = inflater.inflate(R.layout.fragment_main, container, false);
            return  rootView;
        }

        @Override
        public void onAttach(Activity activity) {
            super.onAttach(activity);
//            ((MainActivity) activity).onSectionAttached(
//                    getArguments().getInt(ARG_SECTION_NUMBER));
        }

    }
}
