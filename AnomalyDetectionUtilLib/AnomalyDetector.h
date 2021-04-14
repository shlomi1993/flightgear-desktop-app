/*
 * AnomalyDetector.h
 *
 * Author: Shlomi Ben-Shushan ID: 311408264
 */

#ifndef ANOMALYDETECTOR_H_
#define ANOMALYDETECTOR_H_

#define DEC __declspec( dllexport )

#include <string>
#include <vector>
#include "timeseries.h"
#include "math.h"

using namespace std;

class AnomalyReport{
public:
	const string description;
	const long timeStep;
	AnomalyReport(string description, long timeStep):description(description),timeStep(timeStep){}
};

extern "C" DEC void* AnomalyReport_new(string d, long t) { return (void*) new AnomalyReport(d, t); }

class TimeSeriesAnomalyDetector {
public:
	virtual void learnNormal(const TimeSeries& ts)=0;
	virtual vector<AnomalyReport> detect(const TimeSeries& ts)=0;
	virtual ~TimeSeriesAnomalyDetector(){}
};

//extern "C" DEC void AnomalyReport_learnNormal(const TimeSeries & ts) { return }
//extern "C" DEC vector<AnomalyReport> AnomalyReport_detect
//extern "C" DEC AnomalyReport_del() { }

#endif /* ANOMALYDETECTOR_H_ */
