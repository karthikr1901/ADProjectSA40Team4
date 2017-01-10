package com.example.student.adprojectsa40team4;

import android.annotation.TargetApi;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.Fragment;
import android.content.DialogInterface;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.example.student.adprojectsa40team4.dummy.DummyContent;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * A fragment representing a list of Items.
 * <p/>
 * Large screen devices (such as tablets) are supported by replacing the ListView
 * with a GridView.
 * <p/>
 * Activities containing this fragment MUST implement the {@link OnFragmentInteractionListener}
 * interface.
 */
public class ApproveAdjustmentFragment extends Fragment implements AbsListView.OnItemClickListener {

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
    ListView list_data, list_heading;
    ArrayList<HashMap<String, String>> mylist, mylist_title;
    ListAdapter adapter_title, adapter;
    HashMap<String, String> map_heading, map_list;
    private AbsListView mListView;
    String[] mStringArray;
    ArrayList<AdjustmentVoucherInfo> lastResult;
    ArrayAdapter<String> spinnerArrayAdapter;

    /**
     * The Adapter which will be used to populate the ListView/GridView with
     * Views.
     */
    private ListAdapter mAdapter;
    String voucher_number;
    Spinner spinner;
    Button approveBtn;
    ArrayList<AdjustmentVoucherInfo.AdjustmentVoucherNumber> result;

    // TODO: Rename and change types of parameters
    public static ApproveAdjustmentFragment newInstance(String param1, String param2) {
        ApproveAdjustmentFragment fragment = new ApproveAdjustmentFragment();
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
    public ApproveAdjustmentFragment() {
    }

    @TargetApi(Build.VERSION_CODES.KITKAT)
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // TODO: Change Adapter to display your content
        mAdapter = new ArrayAdapter<DummyContent.DummyItem>(getActivity(),
                android.R.layout.simple_list_item_1, android.R.id.text1, DummyContent.ITEMS);

    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_item, container, false);

        // Set the adapter
        list_heading = (ListView) view.findViewById(R.id.list_heading);
        list_data = (ListView) view.findViewById(R.id.list_data);
        approveBtn = (Button) view.findViewById(R.id.btnApprove);
         spinner = (Spinner) view.findViewById(R.id.voucher_number);

        bindSpinner();

//        new AsyncTask<Void, Void, ArrayList<AdjustmentVoucherInfo.AdjustmentVoucherNumber>>() {
//            @Override
//            protected ArrayList<AdjustmentVoucherInfo.AdjustmentVoucherNumber> doInBackground(Void... params) {
//                ArrayList<AdjustmentVoucherInfo.AdjustmentVoucherNumber> result = AdjustmentVoucherInfo.GetAdjVoucherNumberMobile("7");
//                if (result != null)
//                {
//                    return  result;
//                }
//                else
//                    return  null;
//            }
//            @Override
//            protected void onPostExecute(ArrayList<AdjustmentVoucherInfo.AdjustmentVoucherNumber> voucherNumber) {
//                if (voucherNumber != null) {
//
//                    mStringArray = new String[voucherNumber.size()];
//                    for (int i=0; i <voucherNumber.size();i++)
//                    {
//                        mStringArray[i] = AdjustmentVoucherInfo.list.get(i).toString();
//                    }
//
//                    ArrayAdapter<String> spinnerArrayAdapter = new ArrayAdapter<String>(getActivity(),android.R.layout.simple_spinner_item,mStringArray);
//                    spinnerArrayAdapter.setDropDownViewResource(layout.simple_dropdown_item_1line); // The drop down view
//                    spinner.setAdapter(spinnerArrayAdapter);
//
//                } else {
//                    AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
//                    builder.setMessage("No Adj Voucher to process")
//                            .setCancelable(false)
//                            .setNegativeButton("OK", new DialogInterface.OnClickListener() {
//                                public void onClick(DialogInterface dialog, int id) {
//                                    dialog.cancel();
//                                }
//                            });
//                    AlertDialog alert = builder.create();
//                    alert.show();
//                }
//            }
//        }.execute();

        Log.w("Erorrrrr", "Errorrrr");

       spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                // your code here
                voucher_number = spinner.getItemAtPosition(position).toString();
                new AsyncTask<Void, Void, ArrayList<AdjustmentVoucherInfo>>() {
                    @Override
                    protected ArrayList<AdjustmentVoucherInfo> doInBackground(Void... params) {
                        ArrayList<AdjustmentVoucherInfo> arr = AdjustmentVoucherInfo.getAdjustmentListMobile(voucher_number.toString());
                        if (arr != null)
                        {
                            return  arr;
                        }
                        else
                            return  null;
                    }
                    @Override
                    protected void onPostExecute(ArrayList<AdjustmentVoucherInfo> adjList) {
                        if (adjList != null) {

                            Toast.makeText(getActivity(), voucher_number.toString(), Toast.LENGTH_SHORT).show();
                            mylist = new ArrayList<HashMap<String, String>>();
                            mylist_title = new ArrayList<HashMap<String, String>>();

                            map_heading = new HashMap<String, String>();

                            map_heading.put("code", "Code");
                            map_heading.put("qty", "Qty");
                            map_heading.put("um", "UM");
                            map_heading.put("price", "Price");
                            map_heading.put("amount", "Amount");
                            map_heading.put("reason", "Reason");
                            mylist_title.add(map_heading);

                            adapter_title = new SimpleAdapter(getActivity(),
                                    mylist_title,
                                    R.layout.adjustment_row,
                                    new String[] {"code", "qty", "um", "price" ,"amount","reason"},
                                    new int[]{R.id.col1, R.id.col2, R.id.col3, R.id.col4,R.id.col5,R.id.col6});
                            list_heading.setAdapter(adapter_title);

                            for (int i = 0; i < adjList.size(); i++) {
                                AdjustmentVoucherInfo af = adjList.get(i);
                                map_list = new HashMap<String, String>();
                                map_list.put("code", String.valueOf(af.Description));
                                map_list.put("qty", String.valueOf(af.Balance));
                                map_list.put("um", String.valueOf(af.UOM));
                                map_list.put("price", String.valueOf(af.UnitOfPrice));
                                map_list.put("amount", String.valueOf(af.TotalPrice));
                                map_list.put("reason", String.valueOf(af.Remark));
                                mylist.add(map_list);
                            }

                            adapter = new SimpleAdapter(getActivity(),
                                    mylist,
                                    R.layout.adjustment_row,
                                    new String[]{"code", "qty", "um", "price", "amount", "reason"},
                                    new int[]{R.id.col1, R.id.col2, R.id.col3, R.id.col4, R.id.col5, R.id.col6});
                            Log.w("Eror","Error");
                            list_data.setAdapter(adapter);

                        } else {
                            AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
                            builder.setMessage("No Adj Voucher to process")
                                    .setCancelable(false)
                                    .setNegativeButton("OK", new DialogInterface.OnClickListener() {
                                        public void onClick(DialogInterface dialog, int id) {
                                            dialog.cancel();
                                        }
                                    });
                            AlertDialog alert = builder.create();
                            alert.show();
                        }
                    }
                }.execute();

            }

            @Override
            public void onNothingSelected(AdapterView<?> parentView) {
                // your code here
            }

        });



        approveBtn.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {

                final AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
                builder.setTitle("Are you sure to approve voucher ?");
                builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        new AsyncTask<Void, Void, Integer>() {
                            @Override
                            protected Integer doInBackground(Void... params) {
                                AdjustmentVoucherInfo.ReturnMessage rmsg = AdjustmentVoucherInfo.ApproveAdjustmentVoucher(voucher_number,String.valueOf(LogInModel.empID));
                                if (rmsg.msg.toString().matches("SUCCESS"))
                                    return 1;
                                else
                                    return  0;

                            }
                            @Override
                            protected void onPostExecute(Integer result) {
                                if (result == 1) {
                                    //approveBtn.setEnabled(false);
                                    //mStringArray = null;
                                    Toast.makeText(getActivity(), "Approve Successful", Toast.LENGTH_SHORT).show();
                                    spinnerArrayAdapter.remove(voucher_number);
                                    spinnerArrayAdapter.notifyDataSetChanged();
                                    //bindSpinner();
                                } else {
                                    Toast.makeText(getActivity(), "Approve Fail", Toast.LENGTH_SHORT).show();

                                }
                            }
                        }.execute();
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
            });
        return view;
    }

    public void bindSpinner()
    {
        new AsyncTask<Void, Void, ArrayList<AdjustmentVoucherInfo.AdjustmentVoucherNumber>>() {
            @Override
            protected ArrayList<AdjustmentVoucherInfo.AdjustmentVoucherNumber> doInBackground(Void... params) {
                result = AdjustmentVoucherInfo.GetAdjVoucherNumberMobile(String.valueOf(LogInModel.empRole));
                if (result.size() > 0)
                    return  result;
                else return  null;

            }
            @Override
            protected void onPostExecute(ArrayList<AdjustmentVoucherInfo.AdjustmentVoucherNumber> voucherNumber) {
                if (voucherNumber != null) {

                    mStringArray = new String[voucherNumber.size()];
                    for (int i=0; i <voucherNumber.size();i++)
                    {
                        mStringArray[i] = AdjustmentVoucherInfo.list.get(i).toString();
                    }

                    spinnerArrayAdapter = new ArrayAdapter<String>(getActivity(),android.R.layout.simple_spinner_item,mStringArray);
                    spinnerArrayAdapter.setDropDownViewResource(android.R.layout.simple_dropdown_item_1line); // The drop down view
                    spinner.setAdapter(spinnerArrayAdapter);
                    approveBtn.setEnabled(true);

                } else {
                    AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
                    builder.setMessage("No Adj Voucher to process")
                            .setCancelable(false)
                            .setNegativeButton("OK", new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialog, int id) {
                                    dialog.cancel();
                                }
                            });
                    AlertDialog alert = builder.create();
                    alert.show();
                }
            }
        }.execute();
    }

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

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        if (null != mListener) {
            // Notify the active callbacks interface (the activity, if the
            // fragment is attached to one) that an item has been selected.
            mListener.onFragmentInteraction(DummyContent.ITEMS.get(position).id);
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


}
