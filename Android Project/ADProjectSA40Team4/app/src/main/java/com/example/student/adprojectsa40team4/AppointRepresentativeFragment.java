package com.example.student.adprojectsa40team4;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.net.Uri;
import android.os.Bundle;
import android.app.Fragment;
import android.os.StrictMode;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;


/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link AppointRepresentativeFragment.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link AppointRepresentativeFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class AppointRepresentativeFragment extends Fragment implements View.OnClickListener{
    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;
    Spinner spinner;

    TextView tvName;
    Button btnSubmit;
    String[] empName;
    String empID, RMessage;
    Integer index;

    private OnFragmentInteractionListener mListener;

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment AppointRepresentativeFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static AppointRepresentativeFragment newInstance(String param1, String param2) {
        AppointRepresentativeFragment fragment = new AppointRepresentativeFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    public AppointRepresentativeFragment() {
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
        View v = inflater.inflate(R.layout.fragment_appoint_representative, container, false);

        StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);
        EmployeeModel.EmployeeName ename = EmployeeModel.GetRepresentative(LogInModel.empDept);

        //empID = EmployeeModel.EmployeeID.toString();
        tvName = (TextView) v.findViewById(R.id.tvEName);

        if (!isnull(ename)) {
            tvName.setText(ename.toString());

            ArrayList<EmployeeModel.EmployeeName> namelist = new ArrayList<EmployeeModel.EmployeeName>();
            StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);
            namelist = EmployeeModel.GetEmployeeNameList();

            empName = new String[namelist.size()];

            for (int i = 0; i < namelist.size(); i++) {
                empName[i] = namelist.get(i).toString();
            }

            spinner = (Spinner) v.findViewById(R.id.spnEmployee);
            ArrayAdapter<String> LTRadapter = new ArrayAdapter<String>(this.getActivity(), android.R.layout.simple_spinner_item, empName);
            LTRadapter.setDropDownViewResource(android.R.layout.simple_dropdown_item_1line);
            spinner.setAdapter(LTRadapter);

            btnSubmit = (Button) v.findViewById(R.id.btnSubmit);
            btnSubmit.setOnClickListener(this);

        }
        else {
            Toast.makeText(getActivity().getApplicationContext(), "No Representative.", Toast.LENGTH_SHORT).show();
        }
        return v;
    }

    private boolean isnull(EmployeeModel.EmployeeName ename) {
        return ename == null;
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
        if (v == btnSubmit) {
            AlertDialog.Builder builder = new AlertDialog.Builder(v.getContext());
            builder
                    .setTitle("Appoint Representative")
                    .setMessage("Are you sure you want to appoint this person?")
                    .setPositiveButton("ACCEPT", new DialogInterface.OnClickListener() {
                        public void onClick(DialogInterface dialog, int which) {

                            for (int i = 0; i < empName.length; i++) {
                                if (empName[i] == spinner.getSelectedItem().toString()) {
                                    index = i;
                                }
                            }

                            empID = EmployeeModel.eIdlist.get(index).toString();

                            StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);

                            RMessage = EmployeeModel.ApproveNewRepresentativeMobile(empID, LogInModel.empDept);

                            Toast.makeText(getActivity().getApplicationContext(), RMessage, Toast.LENGTH_SHORT).show();

                            Refresh();

//                            android.app.FragmentManager fragmentManager = getFragmentManager();
//                            ARRFragment fragment=new ARRFragment();
//
//                            fragmentManager.beginTransaction()
//                                    .replace(R.id.container, fragment)
//                                    .commit();
                        }
                    })
                    .setNegativeButton("DECLINE", null)						//Do nothing on no
                    .show();
        }
    }

    public void Refresh(){

        StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);
        EmployeeModel.EmployeeName ename = EmployeeModel.GetRepresentative(LogInModel.empDept);

        tvName.setText(ename.toString());

        ArrayAdapter<String> LTRadapter = new ArrayAdapter<String>(this.getActivity(), android.R.layout.simple_spinner_item, empName);
        LTRadapter.setDropDownViewResource(android.R.layout.simple_dropdown_item_1line);
        spinner.setAdapter(LTRadapter);

        spinner.setFocusable(true);
        spinner.setFocusableInTouchMode(true);
        spinner.requestFocus();

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
        public void onFragmentInteraction(Uri uri);
    }

}
