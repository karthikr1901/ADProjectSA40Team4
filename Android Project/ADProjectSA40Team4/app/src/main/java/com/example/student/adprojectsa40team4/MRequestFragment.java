package com.example.student.adprojectsa40team4;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Fragment;
import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.StrictMode;
import android.text.Editable;
import android.text.InputType;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;


/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link MRequestFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link MRequestFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class MRequestFragment extends Fragment implements View.OnClickListener,AbsListView.OnItemClickListener{
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    private OnFragmentInteractionListener mListener;

    static final String ACTION_SCAN = "com.google.zxing.client.android.SCAN";

    private String codeFormat,codeContent;
    //    private TextView formatTxt, contentTxt;
    TextView tvItemNo,tvRequestNo, tvDescription, tvUnitOfMeasurement;
    EditText etQuantity;
    Button btnScanner,btnAdd,btnSubmit;
    String RequestID="",RMessage="";
    private ListAdapter mAdapter;

    private AbsListView mListView;
    int index;
    List<String> ItemIDList;

    ListView list_Emp,list_heading;
    ArrayList<HashMap<String, String>> mylist, mylist_title;
    ListAdapter adapter_title, adapter;
    HashMap<String, String> map_heading,map_list;


    Bundle savedState;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment MRequestFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static MRequestFragment newInstance(String param1, String param2) {
        MRequestFragment fragment = new MRequestFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public MRequestFragment() {
        // Required empty public constructor
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
        // Inflate the layout for this fragment
        View v = inflater.inflate(R.layout.fragment_mrequest, container, false);
            tvItemNo = (TextView) v.findViewById(R.id.tvItemNo);
            tvRequestNo = (TextView) v.findViewById(R.id.tvRequestNo);
            tvDescription = (TextView) v.findViewById(R.id.tvDescription);
            tvUnitOfMeasurement = (TextView) v.findViewById(R.id.tvUnitOfMeasurement);
            etQuantity = (EditText) v.findViewById(R.id.etQuantity);

            btnScanner = (Button) v.findViewById(R.id.btn_scan_now);
            btnScanner.setFocusable(true);
            btnScanner.setFocusableInTouchMode(true);///add this line
            btnScanner.requestFocus();
            btnScanner.setOnClickListener(this);

            StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);
            RequestID = RequisitionFormInfo.MobileGenReqNoFirst(String.valueOf(LogInModel.empID));
            //tvRequestNo.setText(RequestID);

            list_heading = (ListView) v.findViewById(R.id.list_heading);
            list_Emp = (ListView) v.findViewById(android.R.id.list);

            list_Emp.setOnItemClickListener(this);

            PopulateHeader();

            btnAdd = (Button) v.findViewById(R.id.btnAdd);
            btnAdd.setOnClickListener(this);

            btnSubmit = (Button) v.findViewById(R.id.btnSubmit);
            btnSubmit.setOnClickListener(this);

            TextWatcher textWatcher = new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {
                    //To change body of implemented methods use File | Settings | File Templates.
                }

                @Override
                public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {

                    // after you enter 4 characters into the EditText the soft keyboard must hide
                    if (charSequence.length() == 4){
                        // HIDE the keyboard
                        hideTheKeyboard(getActivity().getApplicationContext(), etQuantity);
                    }
                }

                @Override
                public void afterTextChanged(Editable editable) {
                    //To change body of implemented methods use File | Settings | File Templates.
                }
            };

            etQuantity.addTextChangedListener(textWatcher);


        return  v;
    }

    public void hideTheKeyboard(Context context, EditText editText){
        InputMethodManager imm = (InputMethodManager)context.getSystemService(Context.INPUT_METHOD_SERVICE);
        imm.hideSoftInputFromWindow(editText.getWindowToken(), InputMethodManager.RESULT_UNCHANGED_SHOWN);
    }
    /**
     * Another method to hide the keyboard if the above method is not working.
     */
    public void hideTheKeyboardSecond(EditText editText) {
        editText.setInputType(InputType.TYPE_NULL);
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

    public void PopulateItem(String RequestID,String ItemID, String Quantity){
        mylist.clear();

        ArrayList<RequisitionFormInfo.Mrequest> reqlist = new ArrayList<RequisitionFormInfo.Mrequest>();
        StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);
        reqlist = RequisitionFormInfo.MobileAddItem(RequestID, ItemID, Quantity);

        for (int i = 0; i < reqlist.size(); i++) {
            RequisitionFormInfo.Mrequest rItems = reqlist.get(i);
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

    public void scanNow(View view){
        IntentIntegrator integrator = new IntentIntegrator(this);

        integrator.initiateScan();
    }

    public void onActivityResult(int requestCode, int resultCode, Intent intent) {
        //retrieve scan result
        IntentResult scanningResult = IntentIntegrator.parseActivityResult(requestCode, resultCode, intent);

        if (scanningResult != null) {
            //we have a result
            codeContent = scanningResult.getContents();
            codeFormat = scanningResult.getFormatName();

            tvItemNo.setText(codeContent);

            StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);

            RequisitionFormInfo.MobileGetDetails(tvItemNo.getText().toString());

            tvDescription.setText("   " + RequisitionFormInfo.Description.toString());
            tvUnitOfMeasurement.setText("   " + RequisitionFormInfo.UnitOfMesurement.toString());

        }else{
            Toast toast = Toast.makeText(getActivity().getApplicationContext(),"No scan data received!", Toast.LENGTH_SHORT);
            toast.show();
        }
    }

    public void Refresh(){
        //tvRequestNo.setText("xxx-xxx-xxx");
        tvItemNo.setText("   xxx-xxx-xx   ");
        tvDescription.setText("   xxx-xxx-xx   ");
        tvUnitOfMeasurement.setText("   xxx-xxx-xx   ");
        etQuantity.setText("0");

        btnScanner.setFocusable(true);
        btnScanner.setFocusableInTouchMode(true);///add this line
        btnScanner.requestFocus();
    }

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
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
    public void onClick(View v) {
        try {
            hideTheKeyboard(getActivity().getApplicationContext(), etQuantity);
            if (v == btnScanner) {
                IntentIntegrator integrator = new IntentIntegrator(this);
                integrator.initiateScan();
            } else if (v == btnAdd) {
                StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);
                RequestID = RequisitionFormInfo.MobileGenReqNoSecond(String.valueOf(LogInModel.empID));
                tvRequestNo.setText(RequestID);

                ItemIDList.add(tvItemNo.getText().toString());

                PopulateItem(tvRequestNo.getText().toString(), tvItemNo.getText().toString(), etQuantity.getText().toString());

                Refresh();
            }
            else if (v == btnSubmit) {

                AlertDialog.Builder builder = new AlertDialog.Builder(v.getContext());
                builder
                        .setTitle("Make Request")
                        .setMessage("Are you sure you want to save?")
                        .setPositiveButton("ACCEPT", new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                                StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);

                                RMessage = RequisitionFormInfo.MobileSaveReqNo(tvRequestNo.getText().toString());

                                //if (RMessage == "SUCCESS") {
                                    tvRequestNo.setText("xxx-xxx-xxx");
                                    mylist.clear();
                                    adapter = new SimpleAdapter(getActivity(),
                                            mylist,
                                            R.layout.arrfrow,
                                            new String[]{"Description", "Quantity", "UnitOfMeasurement"},
                                            new int[]{R.id.Description, R.id.Quantity, R.id.UnitOfMeasurement});
                                    list_Emp.setAdapter(adapter);
                                    Refresh();
                                //}
                                Toast.makeText(getActivity().getApplicationContext(), RMessage, Toast.LENGTH_SHORT).show();
                            }
                        })
                        .setNegativeButton("DECLINE", null)						//Do nothing on no
                        .show();

            }


        } catch (ActivityNotFoundException anfe) {
            //on catch, show the download dialog

            //showDialog(getActivity(), "No Scanner Found", "Download a scanner code activity?", "Yes", "No").show();
        }
    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        index = position;

        AlertDialog.Builder builder = new AlertDialog.Builder(view.getContext());
        builder
                .setTitle("Make Request")
                .setMessage("Are you sure you want to delete?")
                .setPositiveButton("ACCEPT", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int which) {

                        StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);

                        RMessage = RequisitionFormInfo.DeleteItem(ItemIDList.get(index).toString(), tvRequestNo.getText().toString());

                        if (RMessage == "true") {
                            mylist.clear();
                            ItemIDList.clear();

                            ArrayList<RequisitionFormInfo.Mrequest> reqlist = new ArrayList<RequisitionFormInfo.Mrequest>();
                            StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);
                            reqlist = RequisitionFormInfo.MobViewEmpNewReq(RequestID);

                            for (int i = 0; i < reqlist.size(); i++) {
                                RequisitionFormInfo.Mrequest rItems = reqlist.get(i);
                                map_list = new HashMap<String, String>();

                                ItemIDList.add(String.valueOf(rItems.ItemNo));
                                map_list.put("Description", String.valueOf(rItems.Description));
                                map_list.put("Quantity", String.valueOf(rItems.Quantity));
                                map_list.put("UnitOfMeasurement", String.valueOf(rItems.UnitOfMeasurement));

                                mylist.add(map_list);
                            }

                            adapter = new SimpleAdapter(getActivity(),
                                    mylist,
                                    R.layout.arrfrow,
                                    new String[]{"Description", "Quantity", "UnitOfMeasurement"},
                                    new int[]{R.id.Description, R.id.Quantity, R.id.UnitOfMeasurement});
                            list_Emp.setAdapter(adapter);

                        } else {
                            mylist.remove(index);
                        }

                        adapter = new SimpleAdapter(getActivity(),
                                mylist,
                                R.layout.arrfrow,
                                new String[]{"Description", "Quantity", "UnitOfMeasurement"},
                                new int[]{R.id.Description, R.id.Quantity, R.id.UnitOfMeasurement});
                        list_Emp.setAdapter(adapter);

                        Toast.makeText(getActivity().getApplicationContext(), "DELETED", Toast.LENGTH_SHORT).show();
                    }
                })
                .setNegativeButton("DECLINE", null)						//Do nothing on no
                .show();


    }

    public static void hideSoftKeyboard(Activity activity) {
        InputMethodManager inputMethodManager = (InputMethodManager)  activity.getSystemService(Activity.INPUT_METHOD_SERVICE);
        inputMethodManager.hideSoftInputFromWindow(activity.getCurrentFocus().getWindowToken(), 0);
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p/>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        public void onFragmentInteraction(Uri uri);
    }

}
