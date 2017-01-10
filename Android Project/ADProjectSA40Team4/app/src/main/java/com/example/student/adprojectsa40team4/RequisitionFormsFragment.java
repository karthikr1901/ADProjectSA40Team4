package com.example.student.adprojectsa40team4;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Fragment;
import android.content.DialogInterface;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.InputType;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import com.example.student.adprojectsa40team4.dummy.dummy.DummyContent;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class RequisitionFormsFragment extends Fragment implements AbsListView.OnItemClickListener  {

    private OnFragmentInteractionListener mListener;

    ListView list_request, list_heading;
    ArrayList<HashMap<String, String>> mylist, mylist_title;
    ListAdapter adapter_title, adapter;
    HashMap<String, String> map_heading, map_list;
    List<String> valuesList = new ArrayList<String>();

    Activity getAct;
    TextView totalCount;

    private AbsListView mListView;
//    static Fragment backFragment;
//    static android.app.FragmentManager fragmentManager;
    private ListAdapter mAdapter;

    public RequisitionFormsFragment() {
    }

    public static RequisitionFormsFragment newInstance(String param1, String param2) {
        RequisitionFormsFragment fragment = new RequisitionFormsFragment();
        Bundle args = new Bundle();
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // TODO: Change Adapter to display your content
        mAdapter = new ArrayAdapter<DummyContent.DummyItem>(getActivity(),
                android.R.layout.simple_list_item_1, android.R.id.text1, DummyContent.ITEMS);

        //PopulateList();

    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        super.onCreateView(inflater,container,savedInstanceState);
        View view = inflater.inflate(R.layout.rquisition_forms, container, false);
        list_request = (ListView) view.findViewById(R.id.list_request);
        list_heading = (ListView) view.findViewById(R.id.list_heading);
        list_request.setOnItemClickListener(this);
        totalCount = (TextView)view.findViewById(R.id.totalCount);
        PopulateList();

//        backFragment = new DisbursementListFragment();
//        fragmentManager = getFragmentManager();
        return view;

    }

    public void PopulateList(){
        mylist = new ArrayList<HashMap<String, String>>();
        mylist_title = new ArrayList<HashMap<String, String>>();

        map_heading = new HashMap<String, String>();

        map_heading.put("desp", "ItemID");
        map_heading.put("qty_hand", "Qty on Hand");
        map_heading.put("needed", "Needed");
        map_heading.put("actual", "Actual");
        mylist_title.add(map_heading);

        getAct = getActivity();

        adapter_title = new SimpleAdapter(getActivity(),
                mylist_title,
                R.layout.request_row,
                new String[] {"desp", "qty_hand", "needed", "actual"},
                new int[]{R.id.col1, R.id.col2, R.id.col3, R.id.col4});
        list_heading.setAdapter(adapter_title);

        new AsyncTask<Void, Void, ArrayList<RequisitionFormInfo>>() {
            @Override
            protected ArrayList<RequisitionFormInfo> doInBackground(Void... params) {
                ArrayList<RequisitionFormInfo> reqList = RequisitionFormInfo.GetRequests();
                return reqList;
            }

            @Override
            protected void onPostExecute(ArrayList<RequisitionFormInfo> reqList) {
               // if (!reqList.isEmpty()) {
                if (reqList.size() > 0) {
                    for (int i = 0; i < reqList.size(); i++) {
                        RequisitionFormInfo rf = reqList.get(i);
                        map_list = new HashMap<String, String>();
                        map_list.put("desp", String.valueOf(rf.itemid));
                        map_list.put("qty_hand", String.valueOf(rf.balance));
                        map_list.put("needed", String.valueOf(rf.tneeded));
                        map_list.put("actual", String.valueOf(rf.talloted));
                        mylist.add(map_list);
                    }
                    Log.w("Errorrrr", "Erorrr");
                    adapter = new SimpleAdapter(getAct,
                            mylist,
                            R.layout.request_row,
                            new String[] {"desp", "qty_hand", "needed", "actual"},
                            new int[]{R.id.col1, R.id.col2, R.id.col3, R.id.col4});
                    list_request.setAdapter(adapter);
                    totalCount.setText("Total Count : " + String.valueOf(mylist.size()));

                } else {
                    Toast.makeText(getActivity(), "No Requests to Process", Toast.LENGTH_SHORT).show();
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

    private Fragment getLastNotNull(List<Fragment> list){
        for (int i= list.size()-1;i>=0;i--){
            Fragment frag = list.get(i);
            if (frag != null){
                return frag;
            }
        }
        return null;
    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        if (null != mListener) {

            map_list = this.mylist.get(position);
            valuesList = getValuesFromHashMap(map_list);

            //empId = String.valueOf(LogInModel.empID);

            final AlertDialog.Builder builder = new AlertDialog.Builder(super.getActivity());
            builder.setTitle("Damage Qty for " + valuesList.get(1).toString());
            final EditText input = new EditText(super.getActivity());
            input.setInputType(InputType.TYPE_CLASS_NUMBER);
            builder.setView(input);

            builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
                String dmgQty;
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    //dmgQty = Integer.parseInt(input.getText().toString());
                    dmgQty = input.getText().toString();
                    int total = Integer.parseInt(map_list.get("qty_hand").toString()) + Integer.parseInt(map_list.get("actual").toString());
                    if (Integer.parseInt(dmgQty) <= total) {

                        new AsyncTask<Void, Void, ArrayList<RequisitionFormInfo>>() {
                            @Override
                            protected ArrayList<RequisitionFormInfo> doInBackground(Void... params) {
                                ArrayList<RequisitionFormInfo> reqList = RequisitionFormInfo.GetUpdateRequests(valuesList.get(1).toString(), dmgQty,String.valueOf(LogInModel.empID));
                                return reqList;
                            }
                            @Override
                            protected void onPostExecute(ArrayList<RequisitionFormInfo> reqList) {
                                if (reqList.size() > 0) {
                                    for (int i = 0; i < reqList.size(); i++) {
                                        RequisitionFormInfo rf = reqList.get(i);
                                        map_list = new HashMap<String, String>();
                                        map_list.put("desp", String.valueOf(rf.itemid));
                                        map_list.put("qty_hand", String.valueOf(rf.balance));
                                        map_list.put("needed", String.valueOf(rf.tneeded));
                                        map_list.put("actual", String.valueOf(rf.talloted));
                                        mylist.add(map_list);
                                    }
                                    PopulateList();

                                } else {
                                    Toast.makeText(getActivity(), "No More Requests to Process", Toast.LENGTH_SHORT).show();
                                }
                            }
                        }.execute();
                    }
                    else
                    {
                        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
                        builder.setMessage("Damage quantity should not be greater than current balance !")
                                .setCancelable(false)
                                .setPositiveButton("Yes", new DialogInterface.OnClickListener() {
                                    public void onClick(DialogInterface dialog, int id) {
                                        dialog.cancel();
                                    }
                                });
                        AlertDialog alert = builder.create();
                        alert.show();
                    }
                }
            });
            builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    dialog.cancel();
                }
            });

            builder.show();
        }
    }

//    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
//
//        if (mListener != null) {
//            final String m_Text = "";
//            AlertDialog.Builder builder = new AlertDialog.Builder(super.getActivity());
//            builder.setTitle("Title");
//
//            // Set up the input
//            final EditText input = new EditText(super.getActivity());
//            // Specify the type of input expected; this, for example, sets the input as a password, and will mask the text
//            input.setInputType(InputType.TYPE_CLASS_TEXT | InputType.TYPE_TEXT_VARIATION_PASSWORD);
//            builder.setView(input);

            // Set up the buttons
//            builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
//                @Override
//                public void onClick(DialogInterface dialog, int which) {
//                    m_Text = input.getText().toString();
//                }
//            });
//            builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
//                @Override
//                public void onClick(DialogInterface dialog, int which) {
//                    dialog.cancel();
//                }
//            });

//        builder.show();
//        }
//    }

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
}
