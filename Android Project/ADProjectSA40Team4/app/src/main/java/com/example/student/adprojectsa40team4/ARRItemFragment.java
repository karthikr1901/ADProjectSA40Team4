package com.example.student.adprojectsa40team4;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.DialogFragment;
import android.content.Context;
import android.content.DialogInterface;
import android.os.Bundle;
import android.app.Fragment;
import android.os.StrictMode;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.app.ListFragment;
import android.widget.Toast;


import com.example.student.adprojectsa40team4.dummy.DummyContent;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * A fragment representing a list of Items.
 * <p>
 * Large screen devices (such as tablets) are supported by replacing the ListView
 * with a GridView.
 * <p>
 * Activities containing this fragment MUST implement the {@link OnFragmentInteractionListener}
 * interface.
 */
public class ARRItemFragment extends ListFragment implements View.OnClickListener {
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;

    /**
     * The fragment's ListView/GridView.
     */
    private AbsListView mListView;

    /**
     * The Adapter which will be used to populate the ListView/GridView with
     * Views.
     */

    //final Context context = this;
    private ListAdapter mAdapter;

    ListView list_Emp,list_heading;
    ArrayList<HashMap<String, String>> mylist, mylist_title;
    ListAdapter adapter_title, adapter;
    HashMap<String, String> map_heading,map_list;

    String RequestID,employeeID,RMessage;

    private Button btnAccept;
    private Button btnReject;
    private EditText etRemark;

    // TODO: Rename and change types of parameters
    public static ARRItemFragment newInstance(String param1, String param2) {
        ARRItemFragment fragment = new ARRItemFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    /**
     * Mandatory empty constructor for the fragment manager to instantiate the
     * fragment (e.g. upon screen orientation changes).
     */
    public ARRItemFragment() {
    }

    @Override
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        if (getArguments() != null) {
            mParam1 = getArguments().getString(ARG_PARAM1);
            mParam2 = getArguments().getString(ARG_PARAM2);
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View myFragmentView = inflater.inflate(R.layout.fragment_arritem, container, false);

        Bundle arg = getArguments();

        if (arg != null) {
            String employeeName = (String) arg.getSerializable("employeeName");
            if (employeeName != null) {

                TextView tvEName = (TextView) myFragmentView.findViewById(R.id.tvEName);
                tvEName.setText(employeeName);
            }

            employeeID = (String) arg.getSerializable("employeeID");
            if (employeeID != null) {

                //Toast.makeText(getActivity().getApplicationContext(),employeeID, Toast.LENGTH_SHORT).show();
            }

            StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);

            RequestID = RequestModel.GetRequestIDByEmployeeID(employeeID);
            Toast.makeText(getActivity().getApplicationContext(),RequestID, Toast.LENGTH_SHORT).show();

            TextView tvReqForm = (TextView) myFragmentView.findViewById(R.id.tvReqForm);
            tvReqForm.setText("#ARR - " + RequestID);
        }

        list_heading = (ListView) myFragmentView.findViewById(R.id.list_heading);
        list_Emp = (ListView) myFragmentView.findViewById(android.R.id.list);

        PopulateHeader();
        PopulateItem(RequestID);

        btnAccept = (Button) myFragmentView.findViewById(R.id.btnAccept);
        btnReject = (Button) myFragmentView.findViewById(R.id.btnReject);
        etRemark = (EditText) myFragmentView.findViewById(R.id.etRemark);

        btnAccept.setOnClickListener(this);
        btnReject.setOnClickListener(this);

        return myFragmentView;
    }

    public void PopulateHeader(){
        mylist = new ArrayList<HashMap<String, String>>();
        mylist_title = new ArrayList<HashMap<String, String>>();
        map_heading = new HashMap<String, String>();


        map_heading.put("Description", "Description");
        map_heading.put("Quantity", "Quantity");
        map_heading.put("Unit Of Mesurement", "Unit Of Mesurement");

        mylist_title.add(map_heading);

        adapter_title = new SimpleAdapter(getActivity(),
                mylist_title,
                R.layout.arrfrow,
                new String[] {"Description", "Quantity", "Unit Of Mesurement"},
                new int[]{R.id.Description, R.id.Quantity, R.id.UnitOfMeasurement});
        list_heading.setAdapter(adapter_title);


    }

    public void PopulateItem(String RequestID){
        ArrayList<RequestModel.RequestItems> reqlist = new ArrayList<RequestModel.RequestItems>();
        StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);
        reqlist = RequestModel.GetRequestItems(RequestID);


        for (int i = 0; i < reqlist.size(); i++) {
            RequestModel.RequestItems rItems = reqlist.get(i);
            map_list = new HashMap<String, String>();

            map_list.put("Description", String.valueOf(rItems.Description));
            map_list.put("Quantity", String.valueOf(rItems.Quantity));
            map_list.put("UnitOfMeasurement", String.valueOf(rItems.UnitOfMeasurement));

            mylist.add(map_list);
        }


        adapter = new SimpleAdapter(getActivity(),
                mylist,
                R.layout.arrfrow,
                new String[] {"Description", "Quantity", "UnitOfMeasurement"},
                new int[]{R.id.Description, R.id.Quantity, R.id.UnitOfMeasurement});
        list_Emp.setAdapter(adapter);
    }

    @Override
    public void onClick(View v) {
        if (v == btnAccept) {

            AlertDialog.Builder builder = new AlertDialog.Builder(v.getContext());
            builder
                    .setTitle("Accept Request")
                    .setMessage("Are you sure you want to accept this Request?")
                    .setIcon(android.R.drawable.ic_dialog_info)
                    .setPositiveButton("ACCEPT", new DialogInterface.OnClickListener() {
                        public void onClick(DialogInterface dialog, int which) {

                            StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);

                            RMessage = RequestModel.UpdateRequestStatusToApprove(RequestID, "APPROVED", employeeID, etRemark.getText().toString());

                            //Toast.makeText(getActivity().getApplicationContext(), RMessage , Toast.LENGTH_SHORT).show();

                            android.app.FragmentManager fragmentManager = getFragmentManager();
                            ARRFragment fragment=new ARRFragment();

                            fragmentManager.beginTransaction()
                                    .replace(R.id.container, fragment)
                                    .commit();
                        }
                    })
                    .setNegativeButton("DECLINE", null)						//Do nothing on no
                    .show();


        }
        else if (v == btnReject)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(v.getContext());
            builder
                    .setTitle("Reject Request")
                    .setMessage("Are you sure you want to reject this Request?")
                    .setIcon(android.R.drawable.ic_dialog_info)
                    .setPositiveButton("ACCEPT", new DialogInterface.OnClickListener() {
                        public void onClick(DialogInterface dialog, int which) {

                            StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);

                            RMessage = RequestModel.UpdateRequestStatusToApprove(RequestID, "REJECTED", employeeID, etRemark.getText().toString());

                            Toast.makeText(getActivity().getApplicationContext(), RMessage , Toast.LENGTH_SHORT).show();

                            android.app.FragmentManager fragmentManager = getFragmentManager();
                            ARRFragment fragment=new ARRFragment();

                            fragmentManager.beginTransaction()
                                    .replace(R.id.container, fragment)
                                    .commit();

                        }
                    })
                    .setNegativeButton("DECLINE", null)						//Do nothing on no
                    .show();

        }

        return;
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

//    @Override
//    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
//        if (null != mListener) {
//            // Notify the active callbacks interface (the activity, if the
//            // fragment is attached to one) that an item has been selected.
//            mListener.onFragmentInteraction(DummyContent.ITEMS.get(position).id);
//        }
//    }

    /**
     * The default content for this Fragment has a TextView that is shown when
     * the list is empty. If you would like to change the text, call this method
     * to supply the text it should use.
     */
    public void setEmptyText(CharSequence emptyText) {
        View emptyView = mListView.getEmptyView();

        if (emptyView instanceof TextView) {
            ((TextView) emptyView).setText(emptyText);
        }
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(String id);
    }

}
