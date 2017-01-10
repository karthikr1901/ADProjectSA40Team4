package com.example.student.adprojectsa40team4;

import android.app.Activity;
import android.app.Fragment;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * A fragment representing a list of Items.
 * <p/>
 * Large screen devices (such as tablets) are supported by replacing the ListView
 * with a GridView.
 * <p/>
 * Activities containing this fragment MUST implement the {@link OnFragmentInteractionListener}
 * interface.
 */
public class CollectionPointsInfoFragment extends Fragment implements AbsListView.OnItemClickListener {

    private OnFragmentInteractionListener mListener;

    /**
     * The fragment's ListView/GridView.
     */
    private AbsListView mListView;
    ListView list_collectionpoint;
    HashMap<String, String> map_list;
    ArrayList<HashMap<String, String>> mylist;
    ListAdapter adapter;
    RadioButton  collectionPointName1,collectionPointName2;
    EmployeeInfo e;
    String pointID,pointID1,pointID2,empMail;
    CollectionPointInfo point;
    List<String> valuesList = new ArrayList<String>();
    /**
     * The Adapter which will be used to populate the ListView/GridView with
     * Views.
     */
    private ListAdapter mAdapter;

    /**
     * Mandatory empty constructor for the fragment manager to instantiate the
     * fragment (e.g. upon screen orientation changes).
     */
    public CollectionPointsInfoFragment() {
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // TODO: Change Adapter to display your content
        mAdapter = new ArrayAdapter<com.example.student.adprojectsa40team4.dummy.dummy.DummyContent.DummyItem>(getActivity(),
                android.R.layout.simple_list_item_1, android.R.id.text1, com.example.student.adprojectsa40team4.dummy.dummy.DummyContent.ITEMS);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View view = inflater.inflate(R.layout.collectionpointinfo_list, container, false);
        list_collectionpoint = (ListView) view.findViewById(R.id.list_collection_points);
        collectionPointName1 = (RadioButton)view.findViewById(R.id.radioPoint1);
        collectionPointName2 = (RadioButton)view.findViewById(R.id.radioPoint2);
        list_collectionpoint.setOnItemClickListener(this);


        new AsyncTask<Void, Void, ArrayList<CollectionPointInfo>>() {
            @Override
            protected ArrayList<CollectionPointInfo> doInBackground(Void... params) {
                int empid = LogInModel.empID;
                String empidStr = String.valueOf(empid);
                ArrayList<CollectionPointInfo> cList = CollectionPointInfo.CollectionPointList(empidStr);
                return cList;
            }

            @Override
            protected void onPostExecute(ArrayList<CollectionPointInfo> cList) {

                if (cList.size() > 0) {
                    for (int i = 0; i < cList.size(); i++) {
                       point = cList.get(i);
                        //point = new CollectionPointInfo(cList.get(i).CollectionPointID.toString(),cList.get(i).Place.toString(),cList.get(i).Time.toString(),cList.get(i).InCharge.toString());
                        String name = point.Place.toString() + " at " + point.Time.toString();
                        if (i == 0)
                        {
                            collectionPointName1.setText(name);
                            pointID1 =  point.CollectionPointID.toString();
                            pointID = point.CollectionPointID.toString();
                        }

                        else if (i == 1)
                        {
                            collectionPointName2.setText(name);
                            pointID2 =  point.CollectionPointID.toString();
                        }

                    }
                    PopulateList();
                } else {
                    Toast.makeText(CollectionPointsInfoFragment.this.getActivity(), getString(R.string.NoCollectionPoint), Toast.LENGTH_SHORT).show();
                }

            }
        }.execute();

        RadioGroup radioGroup = (RadioGroup) view.findViewById(R.id.radioPoints);

        radioGroup.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            public void onCheckedChanged(RadioGroup group, int checkedId) {
                // checkedId is the RadioButton selected

                switch (checkedId) {
                    case R.id.radioPoint1: {
                        // arrDept = new String[]{"POINT11", "POINT12", "POINT13"};
                        pointID = pointID1;
                        PopulateList();
//                        Toast.makeText(getActivity(), "collectionPointName1", Toast.LENGTH_SHORT).show();
                        break;
                    }

                    case R.id.radioPoint2: {
                        //arrDept = new String[]{"POINT21", "POINT22", "POINT23"};
                        pointID = pointID2;
                        PopulateList();
//                        Toast.makeText(getActivity(), "collectionPointName2", Toast.LENGTH_SHORT).show();
                        break;
                    }
                }
            }
        });

        return view;
    }

    public void PopulateList() {
        mylist = new ArrayList<HashMap<String, String>>();
        new AsyncTask<Void, Void, ArrayList<EmployeeInfo>>() {
            @Override
            protected ArrayList<EmployeeInfo> doInBackground(Void... params) {
                ArrayList<EmployeeInfo> empRepList = EmployeeInfo.GetDeptRepListForCollectionPoint(pointID);
                return empRepList;
            }

            @Override
            protected void onPostExecute(ArrayList<EmployeeInfo> empRepList) {
                if (empRepList.size() > 0) {
                    for (int i = 0; i < empRepList.size(); i++) {
                        EmployeeInfo emp = empRepList.get(i);
                        map_list = new HashMap<String, String>();
                        map_list.put("deptID", String.valueOf(emp.DepartmentID));
                        map_list.put("repName", String.valueOf(emp.EmployeeName));
                        map_list.put("email", String.valueOf(emp.EmployeeEmail));
                        mylist.add(map_list);
                    }

                    adapter = new SimpleAdapter(getActivity(),
                            mylist,
                            R.layout.colleciton_point_row,
                            new String[]{"deptID","repName"},
                            new int[]{R.id.col1, R.id.col2});
                    list_collectionpoint.setAdapter(adapter);

                } else {
                }
            }
        }.execute();
    }

    private List<String> getValuesFromHashMap(HashMap<String, String> hashMap) {
        List<String> values = new ArrayList<String>();
        for (String item : hashMap.values()) {
            values.add(item);
        }
        return values;
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            mListener = (OnFragmentInteractionListener) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString()
                    + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        String deptID,repName;
        if (null != mListener) {
            map_list = this.mylist.get(position);
            valuesList = getValuesFromHashMap(map_list);
//            deptID = valuesList.get(1).toString();
//            repName = valuesList.get(0).toString();
            //empMail = valuesList.get(2).toString();

            deptID = map_list.get("deptID").toString();
            repName = map_list.get("repName").toString();
            empMail = map_list.get("email").toString();

            DisbursementListQueryItem item = new DisbursementListQueryItem(deptID,repName,empMail);

            Fragment fragment = new DisbursementListFragment();
            android.app.FragmentManager fragmentManager = getFragmentManager();
            Bundle bundle = new Bundle();
            bundle.putSerializable("item", item);
            fragment.setArguments(bundle);

            fragmentManager.beginTransaction()
                .replace(R.id.container, fragment)
                    .addToBackStack("DisbursementListFragment")
                    .commit();
        }
    }
//
//    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
//        Category category;
//        TextView Id = (TextView)view.findViewById(android.R.id.text1);
//        category = Category.GetCategory(Id.getText().toString());
//        android.app.FragmentManager fragmentManager = getFragmentManager();
//        Fragment fragment = new DetailFragment();
//
//        Bundle bundle = new Bundle();
//
//        bundle.putSerializable("item", category);
//        fragment.setArguments(bundle);
//
//        fragmentManager.beginTransaction()
//                .replace(R.id.container, fragment)
//                .commit();
//
//    }
//});

    public void setEmptyText(CharSequence emptyText) {
        View emptyView = mListView.getEmptyView();

        if (emptyView instanceof TextView) {
            ((TextView) emptyView).setText(emptyText);
        }
    }

    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(String id);
    }

//    public void onRadioButtonClicked(View view) {
//        // Is the button now checked?
//        boolean checked = ((RadioButton) view).isChecked();
//
//        // Check which radio button was clicked
//        switch(view.getId()) {
//            case R.id.radioPoint1:
//                if (checked)
//                {
//                    Toast.makeText(getActivity(), "collectionPointName1", Toast.LENGTH_SHORT).show();
//                    break;
//                }
//            case R.id.radioPoint2:
//                if (checked)
//                {
//                    Toast.makeText(getActivity(), "collectionPointName2", Toast.LENGTH_SHORT).show();
//                    break;
//                }
//
//        }
//    }


//    public void PopulateList(){
//        mylist = new ArrayList<HashMap<String, String>>();
//
//        for (int i = 0; i < arrDept.length; i++) {
//            map_list = new HashMap<String, String>();
//            map_list.put("dept", String.valueOf(arrDept[i]));
//            map_list.put("resName", String.valueOf(arrResName[i]));
//            mylist.add(map_list);
//        }
//
//
//        adapter = new SimpleAdapter(getActivity(),
//                mylist,
//                R.layout.colleciton_point_row,
//                new String[] {"dept", "resName"},
//                new int[]{R.id.col1, R.id.col2});
//        list_collectionpoint.setAdapter(adapter);
//    }


//         empName = LogInModel.empName.toString();
//        new AsyncTask<Void, Void, EmployeeModel>() {
//            @Override
//            protected EmployeeModel doInBackground(Void... params) {
//                //String empName = LogInModel.empName.toString();
//                e = EmployeeModel.GetEmployeeIDByEmployeeName(empName);
//                return e;
//            }
//
//            @Override
//            protected void onPostExecute(EmployeeModel e) {
//
//                if (e != null)
//                {
//                    empID = e.EmployeeID.toString();
//                    new AsyncTask<Void, Void, ArrayList<CollectionPointInfo>>() {
//                        @Override
//                        protected ArrayList<CollectionPointInfo> doInBackground(Void... params) {
//
//                            ArrayList<CollectionPointInfo> cList = CollectionPointInfo.CollectionPointList(empID);
//                            return cList;
//                        }
//
//                        @Override
//                        protected void onPostExecute(ArrayList<CollectionPointInfo> cList) {
//                            CollectionPointInfo point;
//                            if (cList.size() > 0) {
//                                for (int i = 0; i < cList.size(); i++) {
//                                    Log.w("Errorrrrrrrrrrr", "Erorrrrrrrrr");
//                                    point = cList.get(i);
//                                    //point = new CollectionPointInfo(cList.get(i).CollectionPointID.toString(),cList.get(i).Place.toString(),cList.get(i).Time.toString(),cList.get(i).InCharge.toString());
//                                    String name = point.Place.toString();
//                                    if (i == 0)
//                                        collectionPointName1.setText(name);
//                                    else if (i == 1)
//                                        collectionPointName2.setText(name);
//                                }
//                            } else {
//                                Toast.makeText(CollectionPointsInfoFragment.this.getActivity(), getString(R.string.noCollectionPoint), Toast.LENGTH_SHORT).show();
//                            }
//
//                        }
//                    }.execute();
//                }else {
//                    Toast.makeText(CollectionPointsInfoFragment.this.getActivity(), getString(R.string.InvalidemployeeName), Toast.LENGTH_SHORT).show();
//                }
//            }
//        }.execute();

}
