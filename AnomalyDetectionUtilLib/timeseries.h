/*
 * TimeSeries.h
 *
 * Author: Shlomi Ben-Shushan ID: 311408264
 */

#ifndef TIMESERIES_H_
#define TIMESERIES_H_

#define DEC __declspec( dllexport )

#include <iostream>
#include <string.h>
#include <fstream>
#include<map>
#include <vector>
#include <bits/stdc++.h>
#include <algorithm>

using namespace std;

// TimeSeries class.
class TimeSeries{

private:

    // A ts should "know" its features names, their data and the number of samples in the ts (data size).
    vector<string> m_features;
	map<string,vector<float>> m_data;
	size_t m_data_size;

public:

    // Constructor.
    TimeSeries(const char* CSVfileName) {
        // Open input stream.
        ifstream in(CSVfileName);

        // Add m_features and empty float-vectors to the map.
        string first_line, feature;
        in >> first_line;
        stringstream feature_ss(first_line);
        while (getline(feature_ss, feature, ',')) {
            vector<float> feature_data;
            this->m_data[feature] = feature_data;
            this->m_features.push_back(feature);
        }

        // Fill float-vectors with data.
        while (!in.eof()) {
            string row, cell;
            in >> row;
            stringstream row_ss(row);
            size_t i = 0;
            while (getline(row_ss, cell, ',')) {
                this->m_data[m_features[i++]].push_back(stof(cell));
            }
        }

        // Close input stream.
        in.close();

        // Set data-size member.
        this->m_data_size = this->m_data[m_features[0]].size();
    }

    // getFeatureData function gets a feature name and returns the corresponded vector of data.
    const vector<float>& getFeatureData(const string& feature) const {
        return this->m_data.at(feature);
    }

    // getFeaturesNames functions returns the vector that holds the names of the features.
    const vector<string>& getFeaturesNames() const {
        return this->m_features;;
    }

    // getDataSize returns the size, the number of samples, in the ts (lines in CSV, without headers).
    int getDataSize() const {
        return this->m_data_size;
    }

    // Destructor.
	~TimeSeries() = default;


};

extern "C" DEC void* TimeSeries_new(const char* CSVfileName) { return (void*) new TimeSeries(CSVfileName); }
extern "C" DEC const vector<float>& TimeSeries_getFeatureData(TimeSeries * ts, const string & feature) { return ts->getFeatureData(feature); }
extern "C" DEC const vector<string>&TimeSeries_getFeaturesNames(TimeSeries * ts) { return ts->getFeaturesNames(); }
extern "C" DEC int TimeSeries_getDataSize(TimeSeries * ts) { return ts->getDataSize(); }
extern "C" DEC void TimeSeries_del(TimeSeries * ts) { return ts->~TimeSeries(); }



#endif /* TIMESERIES_H_ */
