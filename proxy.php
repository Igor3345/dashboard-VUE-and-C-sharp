<?php

$API_BASE_URL = 'http://localhost:5102/api';
$USERNAME     = 'ivan';
$PASSWORD     = 'password123';

function sendRequest($method, $url, $payload = null, $headers = []) {
    global $API_BASE_URL;
    $ch = curl_init($API_BASE_URL . $url);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, $method);
    
    $defaultHeaders = ['Content-Type: application/json'];
    curl_setopt($ch, CURLOPT_HTTPHEADER, array_merge($defaultHeaders, $headers));
    
    if ($payload) curl_setopt($ch, CURLOPT_POSTFIELDS, $payload);
    
    $response = curl_exec($ch);
    $httpCode = curl_getinfo($ch, CURLINFO_HTTP_CODE);
    curl_close($ch);
    
    return ($httpCode >= 200 && $httpCode < 300) ? json_decode($response, true) : null;
}


$loginPayload = json_encode(['username' => $USERNAME, 'password' => $PASSWORD]);
$loginResponse = sendRequest('POST', '/auth/login', $loginPayload);

if (!isset($loginResponse['token'])) {
    http_response_code(401);
    echo json_encode(['error' => 'Authorization failed']);
    exit;
}
$token = $loginResponse['token'];

$headers = ['Authorization: Bearer ' . $token];
$stats    = sendRequest('GET', '/dashboard/stats', null, $headers);
$tasks    = sendRequest('GET', '/dashboard/tasks', null, $headers) ?: [];
$activity = sendRequest('GET', '/dashboard/activity', null, $headers) ?: [];
$calendar = sendRequest('GET', '/dashboard/calendar?year=2024&month=5', null, $headers) ?: [];


$tasksGrouped = ['High' => [], 'Medium' => [], 'Low' => [], 'Unknown' => []];
foreach ($tasks as $task) {
    $priority = $task['priority'] ?? 'Unknown';
    if (!isset($tasksGrouped[$priority])) $tasksGrouped[$priority] = [];
    $tasksGrouped[$priority][] = $task;
}

$calendarGrouped = [];
foreach ($calendar as $event) {
    $dateStr = $event['eventDate'] ?? '';
    $dateKey = ($dateStr !== '') ? date('Y-m-d', strtotime($dateStr)) : 'unknown_date';
    
    if (!isset($calendarGrouped[$dateKey])) $calendarGrouped[$dateKey] = [];
    $calendarGrouped[$dateKey][] = $event;
}
ksort($calendarGrouped);

$result = [
    'meta' => [
        'generated_at' => date('Y-m-d H:i:s'),
        'user' => $USERNAME
    ],
    'stats' => $stats,
    'tasks_by_priority' => $tasksGrouped,
    'activity' => $activity,
    'calendar_by_date' => $calendarGrouped
];


header('Content-Type: application/json; charset=utf-8');
echo json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);